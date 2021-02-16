using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.DataAccess.MSSQL.Repositories
{
    public class ShowcaseProductRepository : IShowcaseProductRepository
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public ShowcaseProductRepository(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Получение заполненного объема прилавка
        public int GetCurrentFullnessOfShowcase(int showcaseId)
        {
            var showcaseProducts = _context.ShowcaseProducts.Where(x => x.ShowcaseId == showcaseId);
            int result = showcaseProducts.Sum(y => (short)(y.Product.Volume * y.ProductCount));
            return result;
        }

        public ShowcaseProduct[] GetAll()
        {
            var showcaseProducts = _context.ShowcaseProducts.ToArray();
            var mappedShowcaseProducts = _mapper.Map<ShowcaseProduct[]>(showcaseProducts);
            return mappedShowcaseProducts;
        }

        public ShowcaseProduct Get(int showcaseProductId)
        {
            var showcaseProduct = _context.ShowcaseProducts.FirstOrDefault(x => x.Id == showcaseProductId);
            var mappedShowcaseProduct = _mapper.Map<ShowcaseProduct>(showcaseProduct);
            return mappedShowcaseProduct;
        }

        public ShowcaseProduct[] Get(Expression<Func<ShowcaseProduct, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new NullReferenceException("predicate is null");
            }
            var mappedExpression = _mapper.Map<Expression<Func<Entities.ShowcaseProduct, bool>>>(predicate);
            var showcaseProducts = _context.ShowcaseProducts.Where(mappedExpression).ToArray();

            var mappedShowcaseProducts = _mapper.Map<ShowcaseProduct[]>(showcaseProducts);
            return mappedShowcaseProducts;
        }

        public ShowcaseProduct Add(ShowcaseProduct showcaseProduct)
        {
            var mappedShowcaseProduct = _mapper.Map<Entities.ShowcaseProduct>(showcaseProduct);
            _context.ShowcaseProducts.Add(mappedShowcaseProduct);
            _context.SaveChanges();
            var unmappedShowcaseProduct = _mapper.Map<ShowcaseProduct>(mappedShowcaseProduct);
            return unmappedShowcaseProduct;
        }

        public ShowcaseProduct[] Add(ShowcaseProduct[] showcaseProducts)
        {
            var mappedShowcaseProducts = _mapper.Map<Entities.ShowcaseProduct[]>(showcaseProducts);
            _context.ShowcaseProducts.AddRange(mappedShowcaseProducts);
            _context.SaveChanges();
            var unmappedShowcaseProducts = _mapper.Map<ShowcaseProduct[]>(mappedShowcaseProducts);
            return unmappedShowcaseProducts;
        }

        public void Update(ShowcaseProduct showcaseProduct)
        {
            var mappedShowcaseProduct = _mapper.Map<Entities.ShowcaseProduct>(showcaseProduct);
            _context.ShowcaseProducts.Update(mappedShowcaseProduct);
            _context.SaveChanges();
        }

        public void Update(ShowcaseProduct[] showcaseProducts)
        {
            var mappedShowcaseProducts = _mapper.Map<Entities.ShowcaseProduct[]>(showcaseProducts);
            _context.ShowcaseProducts.UpdateRange(mappedShowcaseProducts);
            _context.SaveChanges();
        }

        public void Remove(ShowcaseProduct showcaseProduct)
        {
            var mappedShowcaseProduct = _mapper.Map<Entities.ShowcaseProduct>(showcaseProduct);
            _context.ShowcaseProducts.Remove(mappedShowcaseProduct);
            _context.SaveChanges();
        }

        public void Remove(ShowcaseProduct[] showcaseProducts)
        {
            var mappedShowcaseProducts = _mapper.Map<Entities.ShowcaseProduct[]>(showcaseProducts);
            _context.ShowcaseProducts.RemoveRange(mappedShowcaseProducts);
            _context.SaveChanges();
        }
    }
}
