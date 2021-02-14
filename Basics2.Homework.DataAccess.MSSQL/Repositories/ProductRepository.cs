using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions.Impl;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Basics2.Homework.DataAccess.MSSQL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Product[] GetAll()
        {
            var products = _context.Products.ToArray();
            var mappedProducts = _mapper.Map<Product[]>(products);
            return mappedProducts;
        }

        public Product Get(int productId)
        {
            var product = _context.Products.First(x => x.Id == productId);
            var mappedProduct = _mapper.Map<Product>(product);
            return mappedProduct;
        }

        public Product[] Get(Expression<Func<Product, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new NullReferenceException("predicate is null");
            }
            var mappedExpression = _mapper.Map<Expression<Func<Entities.Product, bool>>>(predicate);
            var products = _context.Products.Where(mappedExpression).ToArray();
            
            var mappedProducts = _mapper.Map<Product[]>(products);
            return mappedProducts;
        }

        public Product Add(Product product)
        {
            var mappedProduct = _mapper.Map<Entities.Product>(product);
            _context.Products.Add(mappedProduct);
            _context.SaveChanges();

            var unmappedProduct = _mapper.Map<Product>(mappedProduct);
            return unmappedProduct; 
        }

        public Product[] Add(Product[] products)
        {
            var mappedProducts = _mapper.Map<Entities.Product[]>(products);
            _context.Products.AddRange(mappedProducts);
            _context.SaveChanges();

            var unmappedProducts = _mapper.Map<Product[]>(mappedProducts);
            return unmappedProducts;
        }

        public void Update(Product product)
        {
            var mappedProduct = _mapper.Map<Entities.Product>(product);
            _context.Products.Update(mappedProduct);
            _context.SaveChanges();
        }

        public void Update(Product[] products)
        {
            var mappedProducts = _mapper.Map<Entities.Product[]>(products);
            _context.Products.UpdateRange(mappedProducts);
            _context.SaveChanges();
        }

        public void Remove(Product product)
        {
            var mappedProduct = _mapper.Map<Entities.Product>(product);
            _context.Products.Remove(mappedProduct);
            _context.SaveChanges();
        }

        public void Remove(Product[] products)
        {
            var mappedProducts = _mapper.Map<Entities.Product>(products);
            _context.Products.RemoveRange(mappedProducts);
            _context.SaveChanges();
        }
    }
}
