using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Models;
using FluentValidation;

namespace Basics2.Homework.Domain.Validation
{
    public class ShowcaseValidation : AbstractValidator<Showcase>
    {
        public ShowcaseValidation()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name should be not null")
                .Length(1,65)
                .WithMessage("Name should be not more than 65 symbols");
            RuleFor(x => x.Volume)
                .GreaterThan((short)0)
                .WithMessage("Volume should be greater than 0");
            RuleFor(x => x.CreateDate)
                .NotEmpty()
                .WithMessage("Create date should be not null")
                .GreaterThan(new DateTime(1900, 01, 01))
                .WithMessage("Create date should be greater than 1900 year");
            RuleFor(x => x.RemoveDate)
                .GreaterThanOrEqualTo(x => x.CreateDate)
                .WithMessage("Remove date should be greater than create date")
                .LessThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("Remove date cannot be more than now date");
        }
    }
}
