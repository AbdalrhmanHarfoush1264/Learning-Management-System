using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.AuthorizationDTOS;

namespace LMSProject.Bussiness.Interfaces
{
    public interface ICusAuthorizationService
    {
        public Task<CusResponse<string>> AddRole(AddRoleRequest request);
        public Task<CusResponse<string>> UpdateRole(UpdateRoleRequest request);
        public Task<CusResponse<string>> DeleteRole(int roleId);
        public Task<CusResponse<List<RoleResonseDto>>> GetRoleList();
        public Task<CusResponse<RoleResonseDto>> GetRoleById(int Id);
        public Task<CusResponse<ManagerUserRolesResponse>> ManagerUserRoles(int userId);
        public Task<CusResponse<string>> UpdateManagerUserRoles(ManagerUserRolesResponse request);
        public Task<CusResponse<ManagerUserClaimsResponse>> ManagerUserClaimsData(int userId);
        public Task<CusResponse<string>> UpdateMangerUserClaims(ManagerUserClaimsResponse request);
    }
}
