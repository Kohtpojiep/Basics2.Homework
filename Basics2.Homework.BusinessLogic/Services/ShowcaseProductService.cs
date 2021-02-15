using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Basics2.Homework.Domain.Validation;

namespace Basics2.Homework.BusinessLogic.Services
{
    public class ShowcaseProductService : IShowcaseProductService
    {
        private readonly IShowcaseProductRepository _showcaseProductRepository;
        private readonly IShowcaseRepository _showcaseRepository;
        private readonly IProductRepository _productRepository;

        public ShowcaseProductService(
            IShowcaseProductRepository showcaseProductRepository, 
            IShowcaseRepository showcaseRepository,
            IProductRepository productRepository)
        {
            _showcaseProductRepository = showcaseProductRepository;
            _showcaseRepository = showcaseRepository;
            _productRepository = productRepository;
        }

        private int GetCurrentVolumeOfShowcase(int showcaseId)
        {
            var showcaseVolume = (int)_showcaseRepository.Get(showcaseId).Volume;
            var existingShowcaseProducts = _showcaseProductRepository.Get(x => x.ShowcaseId == showcaseId);
            foreach (var existingShowcaseProduct in existingShowcaseProducts)
            {
                showcaseVolume -= _productRepository.Get(existingShowcaseProduct.ProductId).Volume * existingShowcaseProduct.ProductCount;
            }

            return showcaseVolume;
        }

        private bool ValidateShowcaseProduct(ShowcaseProduct showcaseProduct)
        {
            ShowcaseProductValidation validation = new ShowcaseProductValidation();
            if (validation.Validate(showcaseProduct).IsValid == false)
                throw new Exception("Объект не прошёл валидацию");
            return true;
        }

        private bool ValidateShowcaseProducts(ShowcaseProduct[] showcaseProducts)
        {
            ShowcaseProductValidation validation = new ShowcaseProductValidation();
            for (int i = 0; i < showcaseProducts.Length; i++)
            {
                if (validation.Validate(showcaseProducts[i]).IsValid == false)
                    throw new Exception("Один из объектов не прошёл валидацию");
            }
            return true;
        }

        public ShowcaseProduct Get(int showcaseProductId)
        {
            if (showcaseProductId < 1)
                throw new Exception("Неккоректный идентификатор");
            return _showcaseProductRepository.Get(showcaseProductId);
        }

        public ShowcaseProduct[] GetAll()
        {
            return _showcaseProductRepository.GetAll();
        }

        public ShowcaseProduct Create(ShowcaseProduct showcaseProduct)
        {
            ValidateShowcaseProduct(showcaseProduct);
            var productsVolume = _productRepository.Get(showcaseProduct.ProductId).Volume * showcaseProduct.ProductCount;
            
            if (productsVolume > GetCurrentVolumeOfShowcase(showcaseProduct.ShowcaseId))
                throw new Exception("Невозможно добавить, витрина будет переполнена");
            return _showcaseProductRepository.Add(showcaseProduct);
        }

        public ShowcaseProduct[] Create(ShowcaseProduct[] showcaseProducts)
        {
            ValidateShowcaseProducts(showcaseProducts);
            return _showcaseProductRepository.Add(showcaseProducts);
        }

        public void Update(ShowcaseProduct showcaseProduct)
        {
            ValidateShowcaseProduct(showcaseProduct);
            _showcaseProductRepository.Update(showcaseProduct);
        }

        public void Update(ShowcaseProduct[] showcaseProducts)
        {
            ValidateShowcaseProducts(showcaseProducts);
            _showcaseProductRepository.Update(showcaseProducts);
        }

        public void Remove(ShowcaseProduct showcaseProduct)
        {
            ValidateShowcaseProduct(showcaseProduct);
            _showcaseProductRepository.Remove(showcaseProduct);
        }

        public void Remove(ShowcaseProduct[] showcaseProducts)
        {
            ValidateShowcaseProducts(showcaseProducts);
            _showcaseProductRepository.Remove(showcaseProducts);
        }
    }
}
