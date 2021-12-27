using P79.Base.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue("Username");
            Fullname = httpContextAccessor.HttpContext?.User?.FindFirstValue("Fullname");
            UnitOfWork = httpContextAccessor.HttpContext?.User?.FindFirstValue("UnitOfWork");
            UnitOfWorkName = httpContextAccessor.HttpContext?.User?.FindFirstValue("UnitOfWorkName");
            JobPosition = httpContextAccessor.HttpContext?.User?.FindFirstValue("JobPosition");
            JobPositionName = httpContextAccessor.HttpContext?.User?.FindFirstValue("JobPositionName");
            RoleId = httpContextAccessor.HttpContext?.User?.FindFirstValue("RoleId");
            RoleName = httpContextAccessor.HttpContext?.User?.FindFirstValue("RoleName");
            GradeId = httpContextAccessor.HttpContext?.User?.FindFirstValue("GradeId");
            GradeName = httpContextAccessor.HttpContext?.User?.FindFirstValue("GradeName");
        }
        public string UserId { get; }
        public string UserName { get; }
        public string Fullname { get; set; }
        public string UnitOfWork { get; set; }
        public string UnitOfWorkName { get; set; }
        public string JobPosition { get; set; }
        public string JobPositionName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string GradeId { get; set; }
        public string GradeName { get; set; }
    }
}
