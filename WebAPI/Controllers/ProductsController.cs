using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        //Inversion of Control --> IoC Container

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _productService.GetAll();
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetByID/{productId}")]
        public IActionResult GetByID(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet("GetAllByCategory/{categoryId}")]
        public IActionResult GetAllByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategory(categoryId);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("GetByUnitPrice")]
        public IActionResult GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productService.GetByUnitPrice(min, max);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("GetProductDetails")]
        public IActionResult GetProductDetails()
        {
            var result = _productService.GetProductDetails();
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var result = _productService.GetByName(name);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("Add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

       

        [HttpPost("Update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpDelete("Delete/{productId}")]
        public IActionResult Delete(int productId)
        {
            Product product = _productService.GetById(productId).Data;
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var result = _productService.Delete(product);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
        }
    }
}
