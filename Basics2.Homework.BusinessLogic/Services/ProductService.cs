using System;
using System.Collections.Generic;
using System.Linq;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.Get(productId);
        }

        public Product[] GetProducts(int[] productIds)
        {
            var productList = new List<Product>();
            for (int i = 0; i < productIds.Length; i++)
            {
               productList.Add(_productRepository.Get(productIds[i]));
            }
            return productList.ToArray();
        }

        public Product CreateProduct(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product[] CreateProducts(Product[] products)
        {
            return _productRepository.Add(products);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }

        public void UpdateProducts(Product[] products)
        {
            _productRepository.Update(products);
        }

        public void RemoveProduct(Product product)
        {
            _productRepository.Remove(product);
        }

        public void RemoveProducts(Product[] products)
        {
            _productRepository.Remove(products);
        }
    }
}