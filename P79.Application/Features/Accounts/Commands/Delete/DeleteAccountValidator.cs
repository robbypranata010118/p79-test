using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application.Features.Commands.Delete
{
    public class DeleteAccountValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} tidak boleh kosong");
        }
    }
}
