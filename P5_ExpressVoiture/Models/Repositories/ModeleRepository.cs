using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class ModeleRepository : Repository<Modele>, IModeleRepository
    {
        public ModeleRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Modele>> GetAllAsync()
        {
            return await _context.Set<Modele>().Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<Modele> GetByIdIncludingSoftDeletedAsync(int id)
        {
            return await _context.Set<Modele>().FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<Modele> GetByIdAsync(int id)
        {
            var modele = await _context.Set<Modele>()
            .FirstOrDefaultAsync(m => m.Id == id && !m.SoftDelete);

            return modele;
        }

        public override void Delete(Modele modele)
        {
            modele.SoftDelete = true;
            _context.Update(modele); // On met à jour l'entité pour marquer comme supprimée
        }
    }
}
