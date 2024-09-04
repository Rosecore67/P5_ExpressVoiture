using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class TypeReparationRepository : Repository<TypeReparation>, ITypeReparationRepository
    {
        public TypeReparationRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<TypeReparation>> GetAllAsync()
        {
            return await _context.Set<TypeReparation>().Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<TypeReparation> GetByIdIncludingSoftDeletedAsync(int id)
        {
            return await _context.Set<TypeReparation>().FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<TypeReparation> GetByIdAsync(int id)
        {
            var typeReparation = await _context.Set<TypeReparation>()
            .FirstOrDefaultAsync(m => m.Id == id && !m.SoftDelete);

            return typeReparation;
        }

        public override void Delete(TypeReparation typeReparation)
        {
            typeReparation.SoftDelete = true;
            _context.Update(typeReparation); // On met à jour l'entité pour marquer comme supprimée
        }
    }
}
