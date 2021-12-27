using UIM.Base.Dtos.Login;
using UIM.Base.Dtos.User;
using UIM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UIM.Base.Interfaces
{
    public interface IUserService
    {
        public Task<User> LoginPublic(LoginRequestDto loginRequest);
        public Task<List<User>> GetUserAdmin(GetUserAdminRequestVm request);
        public Task<int> GetCountUserAdmin(GetUserAdminRequestVm request);
        public Task<User> GetUserByIdAdmin(Guid Id);
        public Task<int> Post(PostUserAdminDto user);
        public Task<int> Put(PutUserAdminDto user);
        public Task<int> Delete(Guid Id);

    }
}
