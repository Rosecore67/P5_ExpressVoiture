using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IFinanceService
    {
        Task<IEnumerable<Finance>> GetAllFinancesAsync();
        Task<Finance> GetFinanceByIdAsync(int id);
        Task AddFinanceAsync(Finance finance);
        Task UpdateFinanceAsync(Finance finance);
        Task DeleteFinanceAsync(int id);
        // Recherche associée à la voiture
        Task<Finance> GetFinanceByVoitureIdAsync(int voitureId);
    }
}
