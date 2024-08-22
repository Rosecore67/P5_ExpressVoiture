using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly ICalculerPrixVenteService _calculerPrixVenteService;

        public FinanceService(IFinanceRepository financeRepository, ICalculerPrixVenteService calculerPrixVenteService)
        {
            _financeRepository = financeRepository;
            _calculerPrixVenteService = calculerPrixVenteService;
        }

        public async Task<Finance> GetFinanceByVoitureIdAsync(int voitureId)
        {
            return await _financeRepository.GetFinanceByVoitureIdAsync(voitureId);
        }

        public async Task<IEnumerable<Finance>> GetAllFinancesAsync()
        {
            return await _financeRepository.GetAllAsync();
        }

        public async Task<Finance> GetFinanceByIdAsync(int id)
        {
            return await _financeRepository.GetByIdAsync(id);
        }

        public async Task AddFinanceAsync(Finance finance)
        {
            finance.PrixVente = await _calculerPrixVenteService.CalculatePrixVenteAsync(finance.VoitureID, finance.PrixAchat ?? 0);
            await _financeRepository.AddAsync(finance);
            await _financeRepository.SaveChangesAsync();
        }

        public async Task UpdateFinanceAsync(Finance finance)
        {
            finance.PrixVente = await _calculerPrixVenteService.CalculatePrixVenteAsync(finance.VoitureID, finance.PrixAchat ?? 0);
            _financeRepository.Update(finance);
            await _financeRepository.SaveChangesAsync();
        }

        public async Task DeleteFinanceAsync(int id)
        {
            var finance = await _financeRepository.GetByIdAsync(id);
            if (finance != null)
            {
                _financeRepository.Delete(finance);
                await _financeRepository.SaveChangesAsync();
            }
        }
    }
}
