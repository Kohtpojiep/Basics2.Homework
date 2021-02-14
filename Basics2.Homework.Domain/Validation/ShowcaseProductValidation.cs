using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;
using FluentValidation;

namespace Basics2.Homework.Domain.Validation
{
    public class ShowcaseProductValidation : AbstractValidator<ShowcaseProduct>
    {
        public ShowcaseProductValidation()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
            RuleFor(x => x.ShowcaseId)
                .GreaterThan(0)
                .WithMessage("Not correct showcase id");
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Not correct product id");
            RuleFor(x => x.ProductCost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cost cannot be smaller than 0");
            RuleFor(x => x.ProductCount)
                .GreaterThan((short)0)
                .WithMessage("Count should be more than 0");
        }
    }
}
