using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;

namespace P5_ExpressVoiture.Models.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.Set<UserRole>().Where(r => !r.SoftDelete).ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllRolesIncludeSoftDeleteAsync()
        {
            return await _context.Set<UserRole>().ToListAsync();
        }

        public async Task<UserRole> GetByIdStringAsync(string id)
        {
            var role = await _context.Set<UserRole>()
                .FirstOrDefaultAsync(r => r.Id == id && !r.SoftDelete);

            return role;
        }

        // Récupérer un rôle par son nom, y compris les soft deleted
        public async Task<UserRole> GetByNameIncludeSoftDeleteAsync(string roleName)
        {
            return await _context.Set<UserRole>().FirstOrDefaultAsync(r => r.Name == roleName);
        }

        // Supprimer définitivement un rôle (hard delete)
        public async Task HardDeleteRoleAsync(UserRole role)
        {
            _context.Set<UserRole>().Remove(role);
            await _context.SaveChangesAsync();
        }

        public override void Delete(UserRole role)
        {
            role.SoftDelete = true;
            _context.Update(role);
        }
    }
}
