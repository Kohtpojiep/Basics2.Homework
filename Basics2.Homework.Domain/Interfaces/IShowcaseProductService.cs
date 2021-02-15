using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IShowcaseProductService
    {
        ShowcaseProduct GetShowcaseProduct(int showcaseProductId);
        ShowcaseProduct[] GetShowcaseProducts(int[] showcaseProductIds);
        ShowcaseProduct CreateShowcaseProduct(ShowcaseProduct showcaseProduct);
        ShowcaseProduct[] CreateShowcaseProducts(ShowcaseProduct[] showcaseProducts);
        void UpdateShowcaseProduct(ShowcaseProduct showcaseProduct);
        void UpdateShowcaseProducts(ShowcaseProduct[] showcaseProducts);
        void RemoveShowcaseProduct(ShowcaseProduct showcaseProduct);
        void RemoveShowcaseProducts(ShowcaseProduct[] showcaseProducts);
    }
}
