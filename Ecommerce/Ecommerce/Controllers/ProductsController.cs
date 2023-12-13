using AutoMapper;
using Ecommerce.Data;
using Ecommerce.Data.Specification;
using Ecommerce.Dtos;
using Ecommerce.Entities;
using Ecommerce.Helpers;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
/*    [Route("api/[controller]")]
    [ApiController]*/
    public class ProductsController : BaseApiController
    {
        #region Implementation
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _produuctTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> ProduuctTypeRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _produuctTypeRepo = ProduuctTypeRepo;
            _mapper = mapper;
        }
        #endregion

        #region GET
        [HttpGet("product")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProductAsync()
        {
            return Ok(await _productRepo.GetProductAsync());
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(productSpecParams);
            var CountSpec = new ProductWithFiltersforCountSpecification(productSpecParams);
            var totalItems = await _productRepo.CountAsync(CountSpec);
            var product = await _productRepo.ListAsync(spec);
            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product);
            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, data));
        }


        [HttpGet("id")]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProductId(int Id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(Id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            /*            var productToReturn = new List<ProductToReturnDto>
                        {
                          new ProductToReturnDto
                          {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            PictureUrl = product.PictureUrl,
                            Price = product.Price,
                            ProductBrand = product.ProductBrand.Name,
                            ProductType = product.ProductType.Name
                          }
                         };*/

            // code for implementing automapper
            var productToReturnList = new List<ProductToReturnDto>
            {
               _mapper.Map<Product, ProductToReturnDto>(product)
            };

            return productToReturnList;
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("BrandTypes")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _produuctTypeRepo.ListAllAsync());
        }

        #endregion
    }


}
