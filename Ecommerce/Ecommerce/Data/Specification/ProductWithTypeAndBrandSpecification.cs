﻿using Ecommerce.Entities;
using System.Linq.Expressions;

namespace Ecommerce.Data.Specification
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productSpecParams)
            : base(x => 
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains
                    (productSpecParams.Search)) && 
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), 
                productSpecParams.PageSize);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price); 
                        break;
                    default:
                        AddOrderBy(n => n.Name); 
                        break;

                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id) 
            : base( x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
