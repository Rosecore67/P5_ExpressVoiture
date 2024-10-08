﻿using Microsoft.AspNetCore.Identity;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;

namespace P5_ExpressVoiture.Models.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly RoleManager<UserRole> _roleManager;

        public UserRoleService(IUserRoleRepository userRoleRepository, RoleManager<UserRole> roleManager)
        {
            _userRoleRepository = userRoleRepository;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserRole>> GetAllRolesAsync()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllRolesIncludesSoftDeleteAsync()
        {
            return await _userRoleRepository.GetAllRolesIncludeSoftDeleteAsync();
        }

        public async Task<UserRole> GetByNameIncludeSoftDeleteAsync(string roleName)
        {
            return await _userRoleRepository.GetByNameIncludeSoftDeleteAsync(roleName);
        }

        public async Task<UserRole> GetRoleByIdAsync(string id)
        {
            return await _userRoleRepository.GetByIdStringAsync(id);
        }

        public async Task<IdentityResult> CreateRoleAsync(UserRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRoleAsync(UserRole role)
        {
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(UserRole role)
        {
            _userRoleRepository.Delete(role);
            await _userRoleRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task HardDeleteRoleAsync(UserRole role)
        {
            await _userRoleRepository.HardDeleteRoleAsync(role);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
