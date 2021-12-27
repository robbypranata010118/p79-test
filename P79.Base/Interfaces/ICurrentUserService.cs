using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Interfaces
{
    public interface ICurrentUserService
    {
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
