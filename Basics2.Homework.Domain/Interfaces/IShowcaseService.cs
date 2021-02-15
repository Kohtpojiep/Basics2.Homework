using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.Domain.Interfaces
{
    public interface IShowcaseService
    {
        Showcase Get(int showcaseId);
        Showcase[] GetAll();
        Showcase Create(Showcase showcase);
        Showcase[] Create(Showcase[] showcases);
        void Update(Showcase showcase);
        void Update(Showcase[] showcases);
        void Remove(Showcase showcase);
        void Remove(Showcase[] showcases);
    }
}
