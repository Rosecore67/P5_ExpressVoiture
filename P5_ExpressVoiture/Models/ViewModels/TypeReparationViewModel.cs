using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class TypeReparationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du type de réparation est requis.")]
        [Display(Name = "Nom du type de réparation")]
        [StringLength(100, ErrorMessage = "Le nom du type de réparation ne doit pas dépasser 100 caractères.")]
        public string NomTypeReparation { get; set; }
    }
}
