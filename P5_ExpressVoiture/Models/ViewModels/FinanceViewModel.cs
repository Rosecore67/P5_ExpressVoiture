using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class FinanceViewModel
    {
        public int VoitureID { get; set; }

        [Required(ErrorMessage = "Le prix d'achat est requis.")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat doit être positif.")]
        public decimal PrixAchat { get; set; }

        public decimal CoutReparationTotal { get; set; }

        public decimal PrixVente => PrixAchat + CoutReparationTotal + 500m;
    }
}
