using UIM.Base.Dtos.RolePermission;
using UIM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIM.Base.Interfaces
{
    public interface IRolePermissionService
    {
        Task<List<RoleAccessViewModel>> GetRolePermissions(string roleId);
        Task<List<PermissioViewModel2>> GetRolePermissionsByRoleAndFeature(string roleId,string feature);
    }
}
