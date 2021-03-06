using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!storeContext.ProductBrands.Any())
                {
                    var brandsJson = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands =  JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
                    foreach(var brand in brands)
                    {
                        storeContext.ProductBrands.Add(brand);
                    }

                    await storeContext.SaveChangesAsync();
                }

                if(!storeContext.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types =  JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    foreach(var type in types)
                    {
                        storeContext.ProductTypes.Add(type);
                    }

                    await storeContext.SaveChangesAsync();
                }

                 if(!storeContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products =  JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach(var product in products)
                    {
                        storeContext.Products.Add(product);
                    }

                    await storeContext.SaveChangesAsync();
                }
            }
            catch(Exception  ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}