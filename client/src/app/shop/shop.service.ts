import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/Pagination';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { shopParams } from '../shared/models/shopParams';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:5001/api/";

  constructor(private http: HttpClient) {
  }

  getProducts(shopParams: shopParams) {
    let params = new HttpParams();
    if (shopParams.brandIdSelected !== 0) {
      params = params.append("brandId", shopParams.brandIdSelected);
    }
    if (shopParams.typeIdSelected !== 0) {
      params = params.append("typeId", shopParams.typeIdSelected);
    }

    if(shopParams.search)
    {
      params =params.append("search", shopParams.search);
    }
    params = params.append("sort", shopParams.sortSelected);
    params = params.append("pageIndex", shopParams.pageNumber);
    params = params.append("pageSize", shopParams.pageSize);

    return this.http.get<IPagination>(this.baseUrl + "products", { observe: 'response', params }).
      pipe(map(response => response.body));
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + "products/brands");
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + "products/types");
  }
}
