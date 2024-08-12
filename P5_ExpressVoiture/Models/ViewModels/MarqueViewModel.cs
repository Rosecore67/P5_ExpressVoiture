using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class MarqueViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la marque est requis.")]
        [StringLength(100, ErrorMessage = "Le nom de la marque ne doit pas dépasser 100 caractères.")]
        public string NomMarque { get; set; }
    }
}
