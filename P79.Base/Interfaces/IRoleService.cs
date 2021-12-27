using UIM.Base.Dtos.Role;
using UIM.Base.Parameters;
using UIM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIM.Base.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleByRoleIdUim(string roleIdUIM);
        Task<List<RoleViewModel2>> GetAllFeatureAndPermissionForAllRole(List<Role> roles);
        Task<RoleViewModel2> GetAllFeatureAndPermissionForSpesificRole(Role role);
    }
}
