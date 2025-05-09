using ReactAPI.Core.Models;

namespace ReactAPI.Core.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<PermissionActions>> GetPermissionTreeAsync(int groupId);
        Task SavePermissionsAsync(int groupId, List<PermissionAction> permissions);
        Task<List<Group>> GetUserGroups();
        Task<List<MenuActionDto>> GetMenuTreeAsync();
    }

}
