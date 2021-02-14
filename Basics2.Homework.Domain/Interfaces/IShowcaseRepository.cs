using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IShowcaseRepository
    {
        Showcase[] GetAll();
        Showcase Get(int showcaseId);
        Showcase[] Get(Expression<Func<Showcase, bool>> predicate);
        Showcase Add(Showcase showcase);
        Showcase[] Add(Showcase[] showcases);
        void Update(Showcase showcase);
        void Update(Showcase[] showcases);
        void Remove(Showcase showcase);
        void Remove(Showcase[] showcases);
    }
}
