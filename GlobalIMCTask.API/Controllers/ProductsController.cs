using GlobalIMCTask.API.ViewModels;
using GlobalIMCTask.API.ViewModels.Products;
using GlobalIMCTask.Common.Errors;
using GlobalIMCTask.Core.Models;
using GlobalIMCTask.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalIMCTask.API.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsLogic _logic;
        private readonly IConfiguration _configs;
        public ProductsController(ProductsLogic logic, IConfiguration configs)
        {
            _logic = logic;
            _configs = configs;
        }
        private int ValidatePageValue(int? page)
        {
            int pageValue = Convert.ToInt32(_configs["pageDefaultValue"]);
            int maxPage = Convert.ToInt32(_configs["maxPageValue"]);
            if (page.HasValue)
            {
                if (page.Value > maxPage)
                    throw new BadRequestException("Pagination", "Page value surpasses maximum allowed page");
                pageValue = page.Value;
            }
            return pageValue;
        }

        private int ValidatePageSizeValue(int? pageSize)
        {
            int pageSizeValue = Convert.ToInt32(_configs["pageSizeDefaultValue"]);
            int maxPageSize = Convert.ToInt32(_configs["maxPageSizeValue"]);

            if (pageSize.HasValue)
            {
                if (pageSize.Value > maxPageSize)
                    throw new BadRequestException("Pagination", "Page size value surpasses maximum allowed page");
                pageSizeValue = pageSize.Value;
            }
            return pageSizeValue;
        }

        private ProductsTableVM preparePagination(Tuple<List<Product>, int> data, int pageValue, int pageSizeValue)
        {
            var results = new ProductsTableVM();

            int total = data.Item2;
            var prods = data.Item1.ConvertProductsToVMs();
            float pages = ((float)total / (float)pageSizeValue);
            results.TotalPages = Math.Ceiling(pages);

            if (((pageValue - 1) * pageSizeValue) + pageSizeValue >= prods.Count())
            {
                results.IsLastPage = true;
            }
            else
            {
                results.IsLastPage = false;
            }
            results.CurrentPage = pageValue;
            results.Products = prods;
            return results;
        }

        [Route("api/products/{page?}/{pageSize?}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200, Type = typeof(ProductsTableVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpGet]
        public IActionResult GetProducts(int? page, int? pageSize)
        {
            try
            {
                int pageValue = ValidatePageValue(page);
                int pageSizeValue = ValidatePageSizeValue(pageSize);
                var data = _logic.GetProducts(pageValue, pageSizeValue);
                var results = preparePagination(data, pageValue, pageSizeValue);
                return Ok(results);
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }

        [Route("api/products/{id}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200, Type = typeof(ProductVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var result = _logic.GetProduct(id).ConvertProductToVM();
                return Ok(result);
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }

        [Route("api/products/title/{title}/{page?}/{pageSize?}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200, Type = typeof(ProductVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpGet]
        public IActionResult GetProductByTitle(string title, int? page, int? pageSize)

        {
            try
            {
                int pageValue = ValidatePageValue(page);
                int pageSizeValue = ValidatePageSizeValue(pageSize);
                var data = _logic.FindProductByTitle(title, pageValue, pageSizeValue);
                var results = preparePagination(data, pageValue, pageSizeValue);
                return Ok(results);
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }


        [Route("api/products/description/{description}/{page?}/{pageSize?}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200, Type = typeof(ProductVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpGet]
        public IActionResult GetProductByDescription(string description, int? page, int? pageSize)

        {
            try
            {
                int pageValue = ValidatePageValue(page);
                int pageSizeValue = ValidatePageSizeValue(pageSize);
                var data = _logic.FindProductByDescription(description, pageValue, pageSizeValue);
                var results = preparePagination(data, pageValue, pageSizeValue);

                return Ok(results);
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }

        [Route("api/products")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpPost]
        public IActionResult CreateProduct(ProductCreationVM product)
        {
            try
            {
                _logic.CreateProduct(product.Title, product.Description, product.ImageURL, product.Price, product.DietaryTypeIds, product.VendorUID);
                return Ok();
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }

        [Route("api/products/{id}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpPut]
        public IActionResult UpdateProduct(int id, [FromBody] ProductCreationVM product)
        {
            try
            {
                _logic.UpdateProduct(id, product.Title, product.Description, product.ImageURL,
                    product.Price, product.DietaryTypeIds, product.VendorUID);
                return Ok();
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }

        [Route("api/products/{id}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _logic.DeleteProduct(id);
                return Ok();
            }
            catch (BadRequestException e)
            {
                return BadRequest(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(new HttpErrorResponse()
                {
                    Message = e.Message,
                    Subject = e.Subject
                });
            }
            catch (Exception e)
            {
                //Log Error
                return StatusCode(500, new HttpErrorResponse()
                {
                    Message = "Internal Error Occurred",
                    Subject = "Internal"
                });
            }
        }
    }
}
