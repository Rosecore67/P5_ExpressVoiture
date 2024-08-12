using System.ComponentModel.DataAnnotations;
using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class VoitureViewModel
    {
        public int Id { get; set; }

        [StringLength(17, ErrorMessage = "Le Code VIN doit comporter 17 caractères.")]
        public string CodeVIN { get; set; }

        [Required(ErrorMessage = "L'année est requise.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Année { get; set; }

        [Required(ErrorMessage = "La marque est requise.")]
        public int MarqueID { get; set; }

        [Required(ErrorMessage = "Le modèle est requis.")]
        public int ModeleID { get; set; }

        [StringLength(50, ErrorMessage = "La finition ne doit pas dépasser 50 caractères.")]
        public string Finition { get; set; }

        [Required(ErrorMessage = "La date d'achat est requise.")]
        [DataType(DataType.Date)]
        public DateTime DateAchat { get; set; }

        public bool EstDisponible { get; set; } = true;

        public string Image { get; set; }

        [Required(ErrorMessage = "Veuillez choisir une image.")]
        public IFormFile ImageFile { get; set; }

        // Pour afficher les informations de la marque et du modèle dans les formulaires
        public IEnumerable<Marque> Marques { get; set; }
        public IEnumerable<Modele> Modeles { get; set; }
    }
}
