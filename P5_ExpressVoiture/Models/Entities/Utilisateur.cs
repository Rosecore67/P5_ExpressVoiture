using Microsoft.AspNetCore.Identity;

namespace P5_ExpressVoiture.Models.Entities
{
    public class Utilisateur : IdentityUser
    {
        public string NomUtilisateur { get; set; }
    }
}
