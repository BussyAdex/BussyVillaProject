using Microsoft.AspNetCore.Identity;

namespace BussyVilla_VillaAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name {  get; set; }
    }
}
