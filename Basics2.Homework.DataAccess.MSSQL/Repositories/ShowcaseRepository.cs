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
    public class ShowcaseRepository : IShowcaseRepository
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public ShowcaseRepository(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Showcase[] GetAll()
        {
            var showcases = _context.Showcases.ToArray();
            var mappedShowcases = _mapper.Map<Showcase[]>(showcases);
            return mappedShowcases;
        }

        public Showcase Get(int showcaseId)
        {
            var showcase = _context.Showcases.FirstOrDefault(x => x.Id == showcaseId);
            var mappedShowcase = _mapper.Map<Showcase>(showcase);
            return mappedShowcase;
        }

        public Showcase[] Get(Expression<Func<Showcase, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new NullReferenceException("predicate is null");
            }
            var mappedExpression = _mapper.Map<Expression<Func<Entities.Showcase, bool>>>(predicate);
            var showcases = _context.Showcases.Where(mappedExpression).ToArray();
            
            var mappedShowcase = _mapper.Map<Showcase[]>(showcases);
            return mappedShowcase;
        }

        public Showcase Add(Showcase showcase)
        {
            var mappedShowcase = _mapper.Map<Entities.Showcase>(showcase);
            _context.Showcases.Add(mappedShowcase);
            _context.SaveChanges();

            var unmappedShowcase = _mapper.Map<Showcase>(mappedShowcase);
            return unmappedShowcase;
        }

        public Showcase[] Add(Showcase[] showcases)
        {
            var mappedShowcases = _mapper.Map<Entities.Product>(showcases);
            _context.Products.AddRange(mappedShowcases);
            _context.SaveChanges();

            var unmappedShowcases = _mapper.Map<Showcase[]>(mappedShowcases);
            return unmappedShowcases;
        }

        public void Update(Showcase showcase)
        {
            var mappedShowcase = _mapper.Map<Entities.Showcase>(showcase);
            _context.Showcases.Update(mappedShowcase);
            _context.SaveChanges();
        }

        public void Update(Showcase[] showcases)
        {
            var mappedShowcases = _mapper.Map<Entities.Showcase>(showcases);
            _context.Showcases.UpdateRange(mappedShowcases);
            _context.SaveChanges();
        }

        public void Remove(Showcase showcase)
        {
            var mappedShowcase = _mapper.Map<Entities.Showcase>(showcase);
            _context.Showcases.Remove(mappedShowcase);
            _context.SaveChanges();
        }

        public void Remove(Showcase[] showcases)
        {
            var mappedShowcases = _mapper.Map<Entities.Showcase>(showcases);
            _context.Showcases.RemoveRange(mappedShowcases);
            _context.SaveChanges();
        }
    }
}
