using ReactAPI.Core.Models;

namespace ReactAPI.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<List<PermissionTreeNode>> GetPermissionTreeAsync(int groupId);
        Task SavePermissionsAsync(int groupId, List<PermissionAction> permissions);
        Task<List<Group>> GetUserGroups();
    }
    
}
