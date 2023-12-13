using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ecommerce.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext)
        {
            if (!storeContext.productBrands.Any())
            {
                var brandData = File.ReadAllText("../Ecommerce/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                storeContext.productBrands.AddRange(brands);
               
            }

            if (!storeContext.productTypes.Any())
            {
                var typesData = File.ReadAllText("../Ecommerce/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                storeContext.productTypes.AddRange(types);
            }

            if (!storeContext.products.Any())
            {
                var products = File.ReadAllText("../Ecommerce/Data/SeedData/Products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(products);
                storeContext.products.AddRange(product);
            }

            storeContext.SaveChanges();

            if (storeContext.ChangeTracker.HasChanges()) await storeContext.SaveChangesAsync();
        }
    }
}
