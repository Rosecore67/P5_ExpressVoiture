using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class MarqueRepository : Repository<Marque>, IMarqueRepository
    {
        public MarqueRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Marque>> GetAllAsync()
        {
            return await _context.Set<Marque>().Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<Marque> GetByIdIncludingSoftDeletedAsync(int id)
        {
            return await _context.Set<Marque>().FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<Marque> GetByIdAsync(int id)
        {
            var marque = await _context.Set<Marque>()
            .FirstOrDefaultAsync(m => m.Id == id && !m.SoftDelete);

            return marque;
        }

        public override void Delete(Marque marque)
        {
            marque.SoftDelete = true;
            _context.Update(marque); // On met à jour l'entité pour marquer comme supprimée
        }
    }
}
