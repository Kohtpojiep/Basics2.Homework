using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IProductService
    {
        Product Get(int productId);
        Product[] GetAll();
        Product Create(Product product);
        Product[] Create(Product[] products);
        void Update(Product product);
        void Update(Product[] products);
        void Remove(Product product);
        void Remove(Product[] products);
    }
}
