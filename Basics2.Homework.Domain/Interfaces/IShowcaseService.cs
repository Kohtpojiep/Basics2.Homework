using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IShowcaseService
    {
        Showcase CreateShowcase(Showcase showcase);
        Showcase[] CreateShowcases(Showcase[] showcases);
        void UpdateShowcase(Showcase showcase);
        void UpdateShowcases(Showcase[] showcases);
        void RemoveShowcase(Showcase showcase);
        void RemoveShowcases(Showcase[] showcases);
    }
}
