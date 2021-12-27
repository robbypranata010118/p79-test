using UIM.Base.Dtos;
using UIM.Base.Dtos.Log;
using UIM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIM.Base.Interfaces
{
    public interface ILogService
    {
        public Task<int> InsertLog(LogDto logDto);
        public Task<List<Log>> GetLog(GetLogRequestVm request);
        public Task<int> GetCountLog(GetLogRequestVm request);
        public Task<Log> GetLogById(Guid Id);
        public Task<List<LovVm>> GetModule();
        public Task<List<LovVm>> GetFeature();
        public Task<List<LovVm>> GetAction();
        public Task<List<LovVm>> GetUser();
    }
}
