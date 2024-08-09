using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class TypeReparationRepository : Repository<TypeReparation>, ITypeReparationRepository
    {
        public TypeReparationRepository(ApplicationDbContext context) : base(context) { }
    }
}
