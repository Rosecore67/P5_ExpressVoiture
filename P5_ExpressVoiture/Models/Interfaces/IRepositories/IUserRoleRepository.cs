using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IRepositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<IEnumerable<UserRole>> GetAllRolesIncludeSoftDeleteAsync();
        Task<UserRole> GetByIdStringAsync(string id);
        Task<UserRole> GetByNameIncludeSoftDeleteAsync(string roleName);
        Task HardDeleteRoleAsync(UserRole role);
    }
}
