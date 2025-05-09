using ReactAPI.Core.Models;

namespace ReactAPI.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<List<PermissionActions>> GetPermissionTreeAsync(int groupId);
        Task SavePermissionsAsync(int groupId, List<PermissionAction> permissions);
        Task<List<Group>> GetUserGroups();
        Task<List<MenuActionDto>> GetMenuTreeAsync();
    }
    
}
