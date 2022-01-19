using FluentValidation;
using PublicForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Domain.Validations
{
    public abstract class TopicValidation : AbstractValidator<Entities.Topic>
    {
        protected void ValidateTitle()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Please specify a Title")
                .MaximumLength(250).WithMessage("Title cannot be longer than 250 characters");
        }

        protected void ValidateDescription()
        {
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Please specify a Description")
                .MaximumLength(10000).WithMessage("Description cannot be longer than 10000 characters");
        }

        protected void ValidateCreateDate()
        {
            RuleFor(t => t.Created)
                .Must(HaveMinimumDate).WithMessage("Please specify a Creation Date Valid")
                .NotNull().WithMessage("Please specify a Creation Date");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumDate(DateTime date)
        {
            return date > DateTime.MinValue;
        }
    }
}
