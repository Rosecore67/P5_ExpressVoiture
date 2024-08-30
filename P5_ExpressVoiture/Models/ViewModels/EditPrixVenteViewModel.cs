using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class EditPrixVenteViewModel
    {
        public int VoitureID { get; set; }

        public int FinanceID {  get; set; }

        [Required(ErrorMessage = "Le prix de vente est requis.")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix de vente doit être un nombre positif.")]
        [Display(Name = "Prix de vente")]
        public decimal PrixVente { get; set; }
    }
}
