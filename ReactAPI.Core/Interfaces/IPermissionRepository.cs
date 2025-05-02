using ReactAPI.Core.Models;

namespace ReactAPI.Core.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<PermissionTreeNode>> GetPermissionTreeAsync(int groupId);
        Task SavePermissionsAsync(int groupId, List<PermissionAction> permissions);
        Task<List<Group>> GetUserGroups();
    }

}
