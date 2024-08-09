using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class ReparationRepository : Repository<Reparation>, IReparationRepository
    {
        public ReparationRepository(ApplicationDbContext context) : base(context) { }
    }
}
