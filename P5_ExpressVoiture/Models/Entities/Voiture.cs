namespace P5_ExpressVoiture.Models.Entities
{
    public class Voiture
    {
        public int Id { get; set; }
        public string? CodeVIN { get; set; }
        public DateTime Année { get; set; }
        public int MarqueID { get; set; }
        public int ModeleID { get; set; }
        public string? Finition { get; set; }
        public DateTime DateAchat { get; set; }
        public DateTime DateDisponibiliteVente { get; set; }
        public DateTime? DateVente { get; set; }
        public bool EstDisponible { get; set; }
        public string? Image { get; set; }

        public virtual IList<Reparation>? Reparations { get; set; }
        public virtual Finance? Finance { get; set; }
        public virtual Marque Marque { get; set; }
        public virtual Modele Modele { get; set; }
    }
}
