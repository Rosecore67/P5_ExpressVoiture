using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class UtilisateurRepository : Repository<Utilisateur>, IUtilisateurRepository
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
