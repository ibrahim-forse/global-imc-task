using GlobalIMCTask.API.ViewModels;
using GlobalIMCTask.API.ViewModels.DietaryTypes;
using GlobalIMCTask.Common.Errors;
using GlobalIMCTask.Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalIMCTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietaryTypesController : ControllerBase
    {
        private readonly ProductsLogic _logic;
        public DietaryTypesController(ProductsLogic logic)
        {
            _logic = logic;
        }

        [Route("api/dietarytypes")]
        [SwaggerOperation(Tags = new[] { "Dietary Types" })]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = 200, Type = typeof(List<DietaryTypeVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, StatusCode = 400, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = 404, Type = typeof(HttpErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = 500, Type = typeof(HttpErrorResponse))]
        [HttpGet]
        public IActionResult GetDietaryTypes()
        {
            try
            {
                var results = _logic.GetDietaryTypes().ConvertDietaryTypesToVMs();
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
    }
}
