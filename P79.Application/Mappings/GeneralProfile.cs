using AutoMapper;
using P79.Base.Dtos.Accounts;
using P79.Base.Dtos.Transactions;
using P79.Domain.Entities;

namespace P79.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<AccountDto,Account>();
            CreateMap<TransactionDto, Domain.Entities.Transaction>();
        }
    }
}
