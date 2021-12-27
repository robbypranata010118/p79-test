using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application.Features.Commands.Deactive
{
    public class DeactiveAccoutnValidator : AbstractValidator<DeactiveAccountCommand>
    {
        public DeactiveAccoutnValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} tidak boleh kosong");
        }
    }
}
