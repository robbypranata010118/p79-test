using FluentValidation;

namespace P79.Application.Features.Commands.Active
{
    public class ActiveAccountValidator : AbstractValidator<ActiveAccountCommand>
    {
        public ActiveAccountValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} tidak boleh kosong");
        }
    }
}
