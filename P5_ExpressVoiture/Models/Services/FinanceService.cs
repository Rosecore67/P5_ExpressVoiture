using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;

        public FinanceService(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
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
            await _financeRepository.AddAsync(finance);
        }

        public async Task UpdateFinanceAsync(Finance finance)
        {
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
