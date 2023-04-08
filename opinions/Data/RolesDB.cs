using Microsoft.AspNetCore.Identity;

namespace opinions.Data
{
    public  static class RolesDB
    {

        public static async Task CreateRolesforApp(IServiceProvider service)
        {

            var userProfileMNG = service.GetService<UserManager<UserProfile>>();
            var userRoleMNG = service.GetService<RoleManager<IdentityRole>>();

            await userRoleMNG.CreateAsync(new IdentityRole("Administrator"));
            var user = new UserProfile
            {
                UserName = "Administrator@iesopinion.com",
                Email = "Administrator@iesopinion.com",
                Name = "Super Admin",
                EmailConfirmed = true
            };
            if (await userProfileMNG.FindByEmailAsync(user.Email) == null)
            {
                await userProfileMNG.CreateAsync(user, "000@Georgian");
                await userProfileMNG.AddToRoleAsync(user, "Administrator");

            }
            await userRoleMNG.CreateAsync(new IdentityRole("PowerUser"));
            var Puser = new UserProfile
            {
                UserName = "PowerUser@iesopinion.com",
                Email = "PowerUser@iesopinion.com",
                Name = "Power User",
                EmailConfirmed = true
            };

            if (await userProfileMNG.FindByEmailAsync(Puser.Email) == null)
            {
                await userProfileMNG.CreateAsync(Puser, "111@Georgian");
                await userProfileMNG.AddToRoleAsync(Puser, "PowerUser");

            }
            await userRoleMNG.CreateAsync(new IdentityRole("User"));
            

            

            
        }


    }
}
