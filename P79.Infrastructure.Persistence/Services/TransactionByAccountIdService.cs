using Microsoft.EntityFrameworkCore;
using P79.Base.Dtos.Transactions;
using P79.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Infrastructure.Persistence.Services
{
    public class TransactionByAccountIdService : ITransactionByAccountId
    {
        private readonly AppDBContext _dbContext;

        public TransactionByAccountIdService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TransactionByAccountIdResponse>> GetTransactionByAccountId(TransactionByAccountIdRequest request)
        {
            return await _dbContext.TransactionByAccountIds.FromSqlInterpolated($"SELECT a.TransactionDate, a.Description, a.Credit, a.Debit, a.Amount FROM ( SELECT a.TransactionDate, a.Description, CAST ( a.Amount AS nvarchar ( MAX )) AS Credit, '-' AS Debit, CAST ( a.Amount AS nvarchar ( MAX )) AS Amount FROM dbo.Transactions AS a WHERE a.DebitCreditStatus = 'C' and a.AccountId = {request.AccountId} and TransactionDate between {request.StartDate.Date} and {request.EndDate.Date} UNION ALL SELECT a.TransactionDate, a.Description, '-' AS Credit, CAST ( a.Amount AS nvarchar ( MAX )) AS Debit, CAST ( a.Amount AS nvarchar ( MAX )) AS Amount FROM dbo.Transactions AS a WHERE a.DebitCreditStatus = 'D' and a.AccountId = {request.AccountId} and TransactionDate between {request.StartDate.Date} and {request.EndDate.Date} ) a ORDER BY a.TransactionDate")
            .ToListAsync();
        }
    }
}
