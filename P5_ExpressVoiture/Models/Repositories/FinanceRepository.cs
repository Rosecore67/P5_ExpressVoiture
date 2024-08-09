using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class FinanceRepository : Repository<Finance>, IFinanceRepository
    {
        public FinanceRepository(ApplicationDbContext context) : base(context) { }
    }
}
