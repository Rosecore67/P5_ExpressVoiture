using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class FinanceViewModel
    {
        public int FinanceId { get; set; }
        public int VoitureID { get; set; }

        [Required(ErrorMessage = "Le prix d'achat est requis.")]
        [Display(Name = "Prix d'achat")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat doit être un nombre positif.")]
        public decimal PrixAchat { get; set; }

        [Display(Name = "Prix de Vente")]
        public decimal PrixVente { get; set; }
    }
}
