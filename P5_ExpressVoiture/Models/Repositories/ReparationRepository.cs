using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class ReparationRepository : Repository<Reparation>, IReparationRepository
    {
        public ReparationRepository(ApplicationDbContext context) : base(context) 
        {
        }

        // Méthode pour obtenir le coût total des réparations d'une voiture
        public async Task<decimal> GetTotalCoutReparationsByVoitureId(int voitureId)
        {
            return await _context.Reparations
                                 .Where(r => r.VoitureID == voitureId)
                                 .SumAsync(r => r.CoutReparation);
        }
    }
}
