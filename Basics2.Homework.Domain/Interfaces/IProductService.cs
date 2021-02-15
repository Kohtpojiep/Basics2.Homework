using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        Product[] GetProducts(int[] productIds);
        Product CreateProduct(Product product);
        Product[] CreateProducts(Product[] products);
        void UpdateProduct(Product product);
        void UpdateProducts(Product[] products);
        void RemoveProduct(Product product);
        void RemoveProducts(Product[] products);
    }
}
