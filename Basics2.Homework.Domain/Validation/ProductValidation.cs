using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;
using FluentValidation;

namespace Basics2.Homework.Domain.Validation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name should be not null");
            RuleFor(x => x.Volume)
                .GreaterThan((short)0)
                .WithMessage("Volume should be greater than 0");
        }
    }
}
