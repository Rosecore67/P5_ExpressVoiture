using Microsoft.AspNetCore.Identity;

namespace P5_ExpressVoiture.Models.Entities
{
    public class UserRole : IdentityRole
    {
        public string NomRole { get; set; }
        public bool SoftDelete { get; set; }
    }
}
