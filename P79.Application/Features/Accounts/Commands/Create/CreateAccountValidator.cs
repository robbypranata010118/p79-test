using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application.Features.Commands.Create
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty().WithMessage("{PropertyName} tidak boleh kosong");
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} tidak boleh kosong");
        }
    }
}
