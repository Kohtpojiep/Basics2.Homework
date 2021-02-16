using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.Configuration.Conventions;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Basics2.Homework.Domain.Validation;
using Basics2.Homework.Domain.Exceptions;

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
                showcaseVolume -= _productRepository.Get(existingShowcaseProduct.ProductId).Volume *
                                  existingShowcaseProduct.ProductCount;
            }
            return showcaseVolume;
        }

        private bool ValidateShowcaseProduct(ShowcaseProduct showcaseProduct)
        {
            var validation = new ShowcaseProductValidation().Validate(showcaseProduct);
            if (validation.IsValid == false)
                throw new ValidationException(string.Join('\n', validation.Errors));
            return true;
        }

        private bool ValidateShowcaseProducts(ShowcaseProduct[] showcaseProducts)
        {
            var validation = new ShowcaseProductValidation();
            for (int i = 0; i < showcaseProducts.Length; i++)
            {
                var resultOfValid = validation.Validate(showcaseProducts[i]);
                if (resultOfValid.IsValid == false)
                    throw new ValidationException(string.Join('\n', resultOfValid.Errors));
            }
            return true;
        }

        public ShowcaseProduct Get(int showcaseProductId)
        {
            if (showcaseProductId < 1)
                throw new ValidationException("Неккоректный идентификатор");
            return _showcaseProductRepository.Get(showcaseProductId);
        }

        public ShowcaseProduct[] GetAll()
        {
            return _showcaseProductRepository.GetAll();
        }

        public ShowcaseProduct Create(ShowcaseProduct showcaseProduct)
        {
            ValidateShowcaseProduct(showcaseProduct);
            var productsVolume =
                _productRepository.Get(showcaseProduct.ProductId).Volume * showcaseProduct.ProductCount;
            int showcaseVolume = _showcaseRepository.Get(showcaseProduct.ShowcaseId).Volume;
            int showcaseFilled = _showcaseProductRepository.GetCurrentFullnessOfShowcase(showcaseProduct.ShowcaseId);
            if (productsVolume > showcaseVolume - showcaseFilled)
                throw new ServiceException("Товар не добавлен, будет переполнение");
            return _showcaseProductRepository.Add(showcaseProduct);
        }

        public ShowcaseProduct[] Create(ShowcaseProduct[] showcaseProducts)
        {
            ValidateShowcaseProducts(showcaseProducts);
            foreach (var showcaseProduct in showcaseProducts)
            {
                var productsVolume = _productRepository.Get(showcaseProduct.ProductId).Volume *
                                     showcaseProduct.ProductCount;
                int showcaseVolume = _showcaseRepository.Get(showcaseProduct.ShowcaseId).Volume;
                int showcaseFilled =
                    _showcaseProductRepository.GetCurrentFullnessOfShowcase(showcaseProduct.ShowcaseId);
                if (productsVolume > showcaseVolume - showcaseFilled)
                    throw new ServiceException("Невозможно добавить, витрина будет переполнена");
            }
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