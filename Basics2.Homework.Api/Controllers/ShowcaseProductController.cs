using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basics2.Homework.Domain.Exceptions;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Basics2.Homework.Domain.Validation;

namespace Basics2.Homework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowcaseProductController : ControllerBase
    {
        private readonly IShowcaseProductService _showcaseProductService;

        /// <summary>
        /// А где это просматривается?...
        /// </summary>
        public ShowcaseProductController(IShowcaseProductService showcaseProductService)
        {
            _showcaseProductService = showcaseProductService;
        }

        /// <summary>
        /// Вывод всех продуктов из прилавков, находящихся в базе данных
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ShowcaseProduct> Get()
        {
            var showcaseProduct = _showcaseProductService.GetAll();
            if (showcaseProduct == null)
                return NotFound();
            return new ObjectResult(showcaseProduct);
        }
        /// <summary>
        /// Вывод информации о продукте прилавка, который обладает данным идентификатором
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/{id}")]
        public ActionResult<ShowcaseProduct> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Error", "Неверный идентификатор");
            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var showcaseProduct = _showcaseProductService.Get(id);
            if (showcaseProduct == null)
                return NotFound();
            return new ObjectResult(showcaseProduct);
        }

        /// <summary>
        /// Добавление продукта на прилавок в базу данных
        /// </summary>
        /// <param name="showcaseProduct"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ShowcaseProduct> Post(ShowcaseProduct showcaseProduct)
        {
            var validation = new ShowcaseProductValidation().Validate(showcaseProduct);
            if (validation.IsValid == false)
            {
                return BadRequest(validation.Errors);
            }
            ShowcaseProduct addedShowcaseProduct;
            try
            {
                addedShowcaseProduct = _showcaseProductService.Create(showcaseProduct);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return new ObjectResult(addedShowcaseProduct);
        }

        /// <summary>
        /// Обновление данных продукта на прилавке в базе данных
        /// </summary>
        /// <param name="showcaseProduct"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put(ShowcaseProduct showcaseProduct)
        {
            var validation = new ShowcaseProductValidation().Validate(showcaseProduct);
            if (validation.IsValid == false)
            {
                return BadRequest();
            }
            try
            {
                _showcaseProductService.Update(showcaseProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Удаление продукта из прилавка
        /// </summary>
        /// <param name="showcaseProduct"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(ShowcaseProduct showcaseProduct)
        {
            var validation = new ShowcaseProductValidation().Validate(showcaseProduct);
            if (validation.IsValid == false)
            {
                return BadRequest();
            }
            try
            {
                _showcaseProductService.Remove(showcaseProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}