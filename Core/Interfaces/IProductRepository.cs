using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<IReadOnlyList<Product>> GetProducts();

        Task<IReadOnlyList<ProductBrand>> GetProductBrands();

        Task<IReadOnlyList<ProductType>> GetProductTypes();
    }
}