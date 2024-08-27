using System.ComponentModel.DataAnnotations;
using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class VoitureViewModel
    {
        public int Id { get; set; }

        [StringLength(17, ErrorMessage = "Le Code VIN doit comporter 17 caractères.")]
        [Display(Name ="Code VIN")]
        public string? CodeVIN { get; set; }

        [Required(ErrorMessage = "L'année est requise.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de mise en circulation")]
        public DateTime Année { get; set; }

        [Required(ErrorMessage = "La marque est requise.")]
        public int MarqueID { get; set; }

        [Required(ErrorMessage = "Le modèle est requis.")]
        public int ModeleID { get; set; }

        [StringLength(50, ErrorMessage = "La finition ne doit pas dépasser 50 caractères.")]
        public string? Finition { get; set; }

        [Required(ErrorMessage = "La date d'achat est requise.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date d'achat")]
        public DateTime DateAchat { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de disponibilité de vente")]
        public DateTime DateDisponibiliteVente { get; set; }

        public bool EstDisponible { get; set; } = true;

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }

        public IEnumerable<Marque>? Marques { get; set; }
        public IEnumerable<Modele>? Modeles { get; set; }
    }
}
