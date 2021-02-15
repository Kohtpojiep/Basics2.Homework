using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IShowcaseProductService
    {
        ShowcaseProduct Get(int showcaseProductId);
        ShowcaseProduct[] GetAll();
        ShowcaseProduct Create(ShowcaseProduct showcaseProduct);
        ShowcaseProduct[] Create(ShowcaseProduct[] showcaseProducts);
        void Update(ShowcaseProduct showcaseProduct);
        void Update(ShowcaseProduct[] showcaseProducts);
        void Remove(ShowcaseProduct showcaseProduct);
        void Remove(ShowcaseProduct[] showcaseProducts);
    }
}
