using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IFinanceRepository : IRepository<Finance>
    {
        // Recherche associée à la voiture
        Task<Finance> GetFinanceByVoitureIdAsync(int voitureId);
    }
}
