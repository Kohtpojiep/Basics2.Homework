using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;

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

        public Showcase CreateShowcase(Showcase showcase)
        { 
            return _showcaseRepository.Add(showcase);
        }

        public Showcase[] CreateShowcases(Showcase[] showcases)
        {
            return _showcaseRepository.Add(showcases);
        }

        public void UpdateShowcase(Showcase showcase)
        {
            _showcaseRepository.Update(showcase);
        }

        public void UpdateShowcases(Showcase[] showcases)
        {
            _showcaseRepository.Update(showcases);
        }

        public void RemoveShowcase(Showcase showcase)
        {
            _showcaseRepository.Remove(showcase);
        }

        public void RemoveShowcases(Showcase[] showcases)
        {
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
