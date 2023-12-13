using Ecommerce.Entities;

namespace Ecommerce.Data.Specification
{
    public class ProductWithFiltersforCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersforCountSpecification(ProductSpecParams productSpecParams)
             : base(x =>
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains
                    (productSpecParams.Search)) &&
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
            
        }
    }
}
