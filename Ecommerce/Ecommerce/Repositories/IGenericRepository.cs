using Ecommerce.Data.Specification;
using Ecommerce.Entities;

namespace Ecommerce.Repositories
{
    public interface IGenericRepository<T> where T : BaseEnitity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);
        Task<IReadOnlyList<Product>> GetProductAsync();
        /*
Task<Product> GetProductByIdAsync(int id);
Task<IReadOnlyList<Product>> GetProductAsync();
Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
Task<IReadOnlyList<ProductType>> GetProductTypeAsync();*/
    }
}
