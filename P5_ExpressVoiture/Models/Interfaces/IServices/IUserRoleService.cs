﻿using Microsoft.AspNetCore.Identity;
using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Models.Interfaces.IServices
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllRolesAsync();
        Task<UserRole> GetRoleByIdAsync(string id);
        Task<IdentityResult> CreateRoleAsync(UserRole role);
        Task<IdentityResult> UpdateRoleAsync(UserRole role);
        Task<IdentityResult> DeleteRoleAsync(UserRole role);
        Task<bool> RoleExistsAsync(string roleName);
    }
}
