using UIM.Base.Dtos.Login;
using UIM.Base.Dtos.User;
using UIM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIM.Base.Interfaces
{
    public interface IUserServicePublic
    {
        public Task<CustomerUser> Login(LoginRequestDto loginRequest);
        public Task<int> SetPassword(SetPasswordDto loginRequest);
    }
}
