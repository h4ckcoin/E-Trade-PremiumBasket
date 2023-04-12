    #nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
    using DataAccess.Contexts;
    using DataAccess.Entities;
using Business.Services;
using Business.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProductModel> productList = _productService.Query().ToList();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductModel product = _productService.Query().SingleOrDefault(p => p.Id == id); 
			if (product == null)
            {
                return NotFound(); 
            }
			return Ok(product); 
        }

		
        [HttpPost]
        public IActionResult Post(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Add(product);
                if (result.IsSuccessful)
                {
					return CreatedAtAction("Get", new { id = product.Id }, product);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest(ModelState);
        }

       
        [HttpPut]
        public IActionResult Put(ProductModel product)
        {
			if (ModelState.IsValid)
			{
				var result = _productService.Update(product);
				if (result.IsSuccessful)
				{
                    return NoContent();
				}
				ModelState.AddModelError("", result.Message);
			}
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
		}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            return NoContent();
        }
	}
}
