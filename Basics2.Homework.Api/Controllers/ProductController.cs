using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Basics2.Homework.Domain.Exceptions;
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
        /// <summary>
        /// А где это просматривается?...
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Вывод всех продуктов, находящихся в базе данных
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Product> Get()
        {
            var product = _productService.GetAll();
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }
        /// <summary>
        /// Вывод продукта, который обладает данным идентификатором
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/{id}")]
        public ActionResult<Product> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Error", "Неверный идентификатор");
            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var product = _productService.Get(id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        /// <summary>
        /// Добавление продукта в базу данных
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            var validation = new ProductValidation().Validate(product);
            if (validation.IsValid == false)
            {
                return BadRequest(validation.Errors);
            }
            Product addedProduct;
            try
            {
                addedProduct = _productService.Create(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return new ObjectResult(addedProduct);
        }

        /// <summary>
        /// Обновление данных продукта в базе данных
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put(Product product)
        {
            var validation = new ProductValidation().Validate(product);
            if (validation.IsValid == false)
            {
                return BadRequest();
            }
            try
            {
                _productService.Update(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Удаление продукта из базы данных
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(Product product)
        {
            var validation = new ProductValidation().Validate(product);
            if (validation.IsValid == false)
            {
                return BadRequest();
            }
            try
            {
                _productService.Remove(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
