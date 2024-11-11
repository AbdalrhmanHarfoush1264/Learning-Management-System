namespace LMSProject.Bussiness.Dtos.AuthorizationDTOS
{
    public class UpdateRoleRequest
    {
        public int Id { get; set; }
        public string roleName { get; set; } = null!;
    }
}
