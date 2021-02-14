using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Basics2.Homework.Domain.Validation;

namespace Basics2.Homework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public Product Post(Product product)
        {
            var validation = new ProductValidation().Validate(product);
            if (validation.IsValid == false)
            {
                throw new Exception(validation.Errors.ToString());
            }

            var addedProduct = _productService.CreateProduct(product);
            return addedProduct;
        }
        
        [HttpPut]
        public void Put(Product product)
        {
            var validation = new ProductValidation().Validate(product);
            if (validation.IsValid == false)
            {
                throw new Exception(validation.Errors.ToString());
            }

            _productService.UpdateProduct(product);
        }
    }
}
