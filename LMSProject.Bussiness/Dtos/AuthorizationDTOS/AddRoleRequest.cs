using System.ComponentModel.DataAnnotations;

namespace LMSProject.Bussiness.Dtos.AuthorizationDTOS
{
    public class AddRoleRequest
    {
        [Required(ErrorMessage = "role name can't be balnk")]
        public string roleName { get; set; } = null!;
    }
}
