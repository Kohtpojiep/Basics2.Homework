using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.DataAccess.MSSQL
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Product, Entities.Product>()
                .ReverseMap();
            CreateMap<Showcase, Entities.Showcase>()
                .ReverseMap();
            CreateMap<ShowcaseProduct, Entities.ShowcaseProduct>()
                .ReverseMap();

            CreateMap<Expression<Func<Product, bool>>, Expression<Func<Entities.Product, bool>>>();
            CreateMap<Expression<Func<Showcase, bool>>, Expression<Func<Entities.Showcase, bool>>>();
            CreateMap<Expression<Func<ShowcaseProduct, bool>>, Expression<Func<Entities.ShowcaseProduct, bool>>>();
        }
    }
}
