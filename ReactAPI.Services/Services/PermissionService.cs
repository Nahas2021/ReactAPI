using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;

namespace ReactAPI.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        
        public async Task<List<PermissionActions>> GetPermissionTreeAsync(int groupId)
        {
            return await _permissionRepository.GetPermissionTreeAsync(groupId);
        }
        public async Task<List<MenuActionDto>> GetMenuTreeAsync()
        {
            return await _permissionRepository.GetMenuTreeAsync();
        }
        public async Task SavePermissionsAsync(int groupId, List<PermissionAction> permissions)
        {
            await _permissionRepository.SavePermissionsAsync(groupId, permissions);
        }
        public async Task<List<Group>> GetUserGroups()
        {
            return await _permissionRepository.GetUserGroups();
        }
    }
}
