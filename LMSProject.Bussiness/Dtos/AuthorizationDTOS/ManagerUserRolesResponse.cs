namespace LMSProject.Bussiness.Dtos.AuthorizationDTOS
{
    public class ManagerUserRolesResponse
    {
        public int UserId { get; set; }
        public List<UserRoles> UserRoles { get; set; } = null!;
    }

    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool HasRole { get; set; }
    }
}
