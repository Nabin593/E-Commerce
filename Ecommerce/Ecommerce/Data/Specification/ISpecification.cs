using System.Linq.Expressions;

namespace Ecommerce.Data.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get;}
        Expression<Func<T, object>> OrderBy {  get; }
        Expression<Func<T, object>> OrderByDescending {  get; }
        #region For pagination  
        int Take { get; }
        int Skip { get; }
        bool IsPaginationEnabled { get; }
        #endregion
    }
}
