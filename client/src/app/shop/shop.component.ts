import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { shopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static:true}) searchTerm: ElementRef;
  
  products: IProduct[];
  brands: IBrand[];
  types: IType[]; 
  shopParams = new shopParams();
  totalCount:number;
  sortOptions=[
    {name:'alphabetical', value:'name'},
    {name:'Price: Low To High', value:'priceAsc'},
    {name:'Price: High To Low', value:'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(
      response => 
      {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize =response.pageSize;
        this.totalCount = response.count;
      },
      error => console.log(error)
    );
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      response => this.brands = [{ id: 0, name: 'All' }, ...response],
      error => console.log(error)
    );
  }

  getTypes() {
    this.shopService.getTypes().subscribe(
      response => this.types = [{ id: 0, name: 'All' }, ...response],
      error => console.log(error)
    );
  }

  onBrandIdSelected(brandId: number) {
    this.shopParams.brandIdSelected = brandId;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onTypeIdSelected(typeId: number) {
    this.shopParams.typeIdSelected = typeId;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onSortSelected(event: Event)
  {
    this.shopParams.sortSelected = (event.target as HTMLInputElement).value;
    this.getProducts();
  }

  onPageChanged(pageNumber:number)
  {
    if(this.shopParams.pageNumber == pageNumber) return;
    
    this.shopParams.pageNumber =pageNumber;    
    this.getProducts();
  }

  onSearch()
  {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onReset()
  {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new shopParams();
    this.getProducts();
  }
}
