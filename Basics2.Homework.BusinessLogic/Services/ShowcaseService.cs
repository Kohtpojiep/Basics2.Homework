using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Basics2.Homework.Domain.Validation;

namespace Basics2.Homework.BusinessLogic.Services
{
    public class ShowcaseService : IShowcaseService
    {
        private readonly IShowcaseRepository _showcaseRepository;
        private readonly IShowcaseProductRepository _showcaseProductRepository;

        public ShowcaseService(IShowcaseRepository showcaseRepository, IShowcaseProductRepository showcaseProductRepository)
        {
            _showcaseRepository = showcaseRepository;
            _showcaseProductRepository = showcaseProductRepository;
        }
        /*
        #Работа с витринами
        1) Создать новую витрину с названием и объемом. Объем - сколько товаров может поместиться, у каждого товара свой объем.
        2) Отредактировать витрину. При смене объема, если товары уже расположены на витрине и их суммарный объем больше нового значения объема витрины, апи должен отвечать ошибкой.
        3) Удалить витрину. Если товары расположены на витрине, то апи должен отвечать ошибкой.
        4) В каждую созданную витрину можно добавить товар. При этом если товар не помещается, апи должен отвечать ошибкой.
        */

        private bool ValidateShowcase(Showcase showcase)
        {
            ShowcaseValidation validation = new ShowcaseValidation();
            if (validation.Validate(showcase).IsValid == false)
                throw new Exception("Один из объектов не прошёл валидацию");
            return true;
        }

        private bool ValidateShowcases(Showcase[] showcases)
        {
            ShowcaseValidation validation = new ShowcaseValidation();
            for (int i = 0; i < showcases.Length; i++)
            {
                if (validation.Validate(showcases[i]).IsValid == false)
                    throw new Exception("Один из объектов не прошёл валидацию");
            }
            return true;
        }

        public Showcase Get(int showcaseId)
        {
            if (showcaseId < 1)
                throw new Exception("Неккоректный идентификатор");
            return _showcaseRepository.Get(showcaseId);
        }

        public Showcase[] GetAll()
        {
            return _showcaseRepository.GetAll();
        }
        public Showcase Create(Showcase showcase)
        {
            ValidateShowcase(showcase);
            return _showcaseRepository.Add(showcase);
        }

        public Showcase[] Create(Showcase[] showcases)
        {
            ValidateShowcases(showcases);
            return _showcaseRepository.Add(showcases);
        }

        public void Update(Showcase showcase)
        {
            ValidateShowcase(showcase);
            _showcaseRepository.Update(showcase);
        }

        public void Update(Showcase[] showcases)
        {
            ValidateShowcases(showcases);
            _showcaseRepository.Update(showcases);
        }

        public void Remove(Showcase showcase)
        {
            ValidateShowcase(showcase);
            if (_showcaseProductRepository.Get(x => x.ShowcaseId == showcase.Id).Length != 0)
            {
                throw new Exception("В прилавке есть товары");
            }
            _showcaseRepository.Remove(showcase);
        }

        public void Remove(Showcase[] showcases)
        {
            ValidateShowcases(showcases);
            for (int i = 0; i < showcases.Length; i++)
            {
                if (_showcaseProductRepository.Get(x => x.ShowcaseId == showcases[i].Id).Length != 0)
                {
                    throw new Exception("В одном из прилавков есть товары");
                }
            }
            _showcaseRepository.Remove(showcases);
        }
    }
}
