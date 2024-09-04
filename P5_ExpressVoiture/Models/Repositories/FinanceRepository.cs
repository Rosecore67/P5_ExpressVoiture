using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class FinanceRepository : Repository<Finance>, IFinanceRepository
    {
        public FinanceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Finance> GetFinanceByVoitureIdAsync(int voitureId)
        {
            return await _context.Finances.FirstOrDefaultAsync(f => f.VoitureID == voitureId);
        }
    }
}
