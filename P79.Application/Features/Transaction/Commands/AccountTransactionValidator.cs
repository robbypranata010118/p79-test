using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application.Features.Transaction.Commands
{
    public class AccountTransactionValidator : AbstractValidator<AccountTransactionCommand>
    {
        public AccountTransactionValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TransactionDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.DebitCreditStatus).NotEmpty()
                .Must(CheckStatus).WithMessage("Value must be C for Credit or D for Debit");
        }

        private bool CheckStatus(string val)
        {
            if (val == "C" || val == "D")
                return true;
            return false;
        }
    }
}
