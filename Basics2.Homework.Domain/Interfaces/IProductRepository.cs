using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IProductRepository
    {
        Product[] GetAll();
        Product Get(int productId);
        Product[] Get(Expression<Func<Product, bool>> predicate);
        Product Add(Product product);
        Product[] Add(Product[] products);
        void Update(Product product);
        void Update(Product[] products);
        void Remove(Product product);
        void Remove(Product[] products);
    }
}
