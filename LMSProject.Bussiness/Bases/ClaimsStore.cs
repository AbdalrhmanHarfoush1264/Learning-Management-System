using System.Security.Claims;

namespace LMSProject.Bussiness.Bases
{
    public static class ClaimsStore
    {

        public static List<Claim> Claims = new List<Claim>()
        {
            new Claim("Create Student","false"),
            new Claim("Update Student","false"),
            new Claim("Delete Student","false"),
            new Claim("Get Students List","false"),
            new Claim("Get Student By Id","false"),
            new Claim("Get Users List","false"),
            new Claim("Get User By Id","false")
        };
    }
}
