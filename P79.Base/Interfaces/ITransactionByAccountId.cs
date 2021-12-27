using P79.Base.Dtos.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Interfaces
{
    public interface ITransactionByAccountId
    {
        Task<List<TransactionByAccountIdResponse>> GetTransactionByAccountId(TransactionByAccountIdRequest request);
    }
}
