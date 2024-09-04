using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class VoitureRepository : Repository<Voiture>, IVoitureRepository
    {
        public VoitureRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Voiture> GetByVinAsync(string vin)
        {
            return await _context.Voitures.FirstOrDefaultAsync(v => v.CodeVIN == vin);
        }
    }
}
