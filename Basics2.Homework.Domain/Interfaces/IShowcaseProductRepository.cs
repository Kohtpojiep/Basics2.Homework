using System;
using System.Linq;
using System.Linq.Expressions;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IShowcaseProductRepository
    {
        int GetCurrentFullnessOfShowcase(int showcaseId);
        ShowcaseProduct[] GetAll();
        ShowcaseProduct Get(int showcaseProductId);
        ShowcaseProduct[] Get(Expression<Func<ShowcaseProduct, bool>> predicate);
        ShowcaseProduct Add(ShowcaseProduct showcaseProduct);
        ShowcaseProduct[] Add(ShowcaseProduct[] showcaseProducts);
        void Update(ShowcaseProduct showcaseProduct);
        void Update(ShowcaseProduct[] showcaseProducts);
        void Remove(ShowcaseProduct showcaseProduct);
        void Remove(ShowcaseProduct[] showcaseProducts);
    }
}