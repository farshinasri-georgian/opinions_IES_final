using Microsoft.AspNetCore.Identity;

namespace opinions.Data
{
    public class UserProfile:IdentityUser
    {
        public string? Name { get; set; }
        public int? studentID { get; set; }
    }
}
