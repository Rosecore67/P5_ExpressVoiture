using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IReparationService
    {
        Task<IEnumerable<Reparation>> GetAllReparationsAsync();
        Task<Reparation> GetReparationByIdAsync(int id);
        Task AddReparationAsync(Reparation reparation);
        Task UpdateReparationAsync(Reparation reparation);
        Task DeleteReparationAsync(int id);

        // Méthode pour obtenir le coût total des réparations
        Task<decimal> GetTotalCoutReparationsByVoitureId(int voitureId);
        // Méthode pour obtenir les réparations d'un véhicule
        Task<IEnumerable<Reparation>> GetReparationsByVoitureIdAsync(int voitureId);
    }
}
