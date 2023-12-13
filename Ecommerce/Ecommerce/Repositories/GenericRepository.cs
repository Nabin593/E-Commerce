using Ecommerce.Data;
using Ecommerce.Data.Specification;
using Ecommerce.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEnitity
    {
        private readonly StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _storeContext.Set<T>().FirstAsync(x => x.Id == id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
            return _storeContext.Set<Product>().ToList();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
           return _storeContext.Set<T>().ToList();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), spec);
        }
    }
}
