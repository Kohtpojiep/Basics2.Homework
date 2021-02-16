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
    public class ShowcaseController : ControllerBase
    {
        private readonly IShowcaseService _showcaseService;

        public ShowcaseController(IShowcaseService showcaseService)
        {
            _showcaseService = showcaseService;
        }

        /// <summary>
        /// Вывод всех прилавков, находящихся в базе данных
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Showcase> Get()
        {
            var showcase = _showcaseService.GetAll();
            if (showcase == null)
                return NotFound();
            return new ObjectResult(showcase);
        }
        /// <summary>
        /// Вывод прилавка, который обладает данным идентификатором
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/{id}")]
        public ActionResult<Showcase> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Error", "Неверный идентификатор");
            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var showcase = _showcaseService.Get(id);
            if (showcase == null)
                return NotFound();
            return new ObjectResult(showcase);
        }

        /// <summary>
        /// Добавление прилавка в базу данных
        /// </summary>
        /// <param name="showcase"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Showcase> Post(Showcase showcase)
        {
            var validation = new ShowcaseValidation().Validate(showcase);
            if (validation.IsValid == false)
            {
                return BadRequest(validation.Errors);
            }
            Showcase addedShowcase;
            try
            {
                addedShowcase = _showcaseService.Create(showcase);
            }
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return new ObjectResult(addedShowcase);
        }

        /// <summary>
        /// Обновление данных прилавка в базе данных
        /// </summary>
        /// <param name="showcase"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put(Showcase showcase)
        {
            var validation = new ShowcaseValidation().Validate(showcase);
            if (validation.IsValid == false)
            {
                return BadRequest();
            }
            try
            {
                _showcaseService.Update(showcase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Удаление прилавка из базы данных
        /// </summary>
        /// <param name="showcase"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(Showcase showcase)
        {
            var validation = new ShowcaseValidation().Validate(showcase);
            if (validation.IsValid == false)
            {
                return BadRequest();
            }
            try
            {
                _showcaseService.Remove(showcase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
