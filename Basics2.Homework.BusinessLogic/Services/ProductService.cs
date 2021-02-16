using System;
using System.Collections.Generic;
using System.Linq;
using Basics2.Homework.Domain.Exceptions;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Basics2.Homework.Domain.Validation;

namespace Basics2.Homework.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        private bool ValidateProduct(Product product)
        {
            ProductValidation validation = new ProductValidation();
            if (validation.Validate(product).IsValid == false)
                throw new ValidationException("Один из объектов не прошёл валидацию");
            return true;
        }

        private bool ValidateProducts(Product[] products)
        {
            ProductValidation validation = new ProductValidation();
            for (int i = 0; i < products.Length; i++)
            {
                if (validation.Validate(products[i]).IsValid == false)
                    throw new ValidationException("Один из объектов не прошёл валидацию");
            }
            return true;
        }

        public Product Get(int productId)
        {
            if (productId < 1)
                throw new ValidationException("Неккоректный идентификатор");
            return _productRepository.Get(productId);
        }

        public Product[] GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product Create(Product product)
        {
            ValidateProduct(product);
            return _productRepository.Add(product);
        }

        public Product[] Create(Product[] products)
        {
            ValidateProducts(products);
            return _productRepository.Add(products);
        }

        public void Update(Product product)
        {
            ValidateProduct(product);
            _productRepository.Update(product);
        }

        public void Update(Product[] products)
        {
            ValidateProducts(products);
            _productRepository.Update(products);
        }

        public void Remove(Product product)
        {
            ValidateProduct(product);
            _productRepository.Remove(product);
        }

        public void Remove(Product[] products)
        {
            ValidateProducts(products);
            _productRepository.Remove(products);
        }
    }
}