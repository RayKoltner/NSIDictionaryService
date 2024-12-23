using Microsoft.AspNetCore.Identity;

namespace NSIDictionaryService.Data.Models.Users
{
    public class Role: IdentityRole<int>
    {
        public Role(string role): base(role) { }
    }
}
