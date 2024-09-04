using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IReparationRepository : IRepository<Reparation>
    {
        // Méthode pour obtenir le coût total des réparations
        Task<decimal> GetTotalCoutReparationsByVoitureId(int voitureId);
        // Méthode pour obtenir les réparations d'un véhicule
        Task<IEnumerable<Reparation>> GetReparationsByVoitureIdAsync(int voitureId);
    }
}
