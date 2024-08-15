using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class VoitureIndexViewModel
    {
        public int Id { get; set; }
        public string CodeVIN { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public DateTime Année { get; set; }
        public string Finition { get; set; }
        public DateTime DateAchat { get; set; }
        public DateTime DateDisponibiliteVente { get; set; }
        public bool EstDisponible { get; set; }
        public string Image { get; set; }
        public decimal? PrixVente { get; set; }

        public IEnumerable<Marque> Marques { get; set; }
        public IEnumerable<Modele> Modeles { get; set; }
    }
}
