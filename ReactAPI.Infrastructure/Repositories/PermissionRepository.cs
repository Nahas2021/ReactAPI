using Microsoft.EntityFrameworkCore;
using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Infrastructure.Data;

namespace ReactAPI.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly EmployeeContext _context;

        public PermissionRepository(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<List<Group>> GetUserGroups()
        {
            var groups = await _context.Groups
              .Select(g => new Group
              {
                  GroupId = g.GroupId,
                  GroupName = g.GroupName
              })
              .ToListAsync();

            return groups;
        }
            public async Task<List<PermissionTreeNode>> GetPermissionTreeAsync(int groupId)
        {
            var menus = await _context.Menus.ToListAsync();
            var actions = await _context.Actions.ToListAsync();
            var groupPermissions = await _context.GroupPermissions
                .Where(gp => gp.GroupId == groupId)
                .ToListAsync();

            List<PermissionTreeNode> BuildTree(int? parentId)
            {
                return menus
                    .Where(m => m.ParentMenuId == parentId)
                    .Select(m => new PermissionTreeNode
                    {
                        MenuId = m.MenuId,
                        MenuName = m.MenuName,
                        Route = m.Route,
                        Actions = groupPermissions
                            .Where(gp => gp.MenuId == m.MenuId)
                            .Join(actions, gp => gp.ActionId, a => a.ActionId, (gp, a) => a.ActionName)
                            .ToList(),
                        Children = BuildTree(m.MenuId)
                    })
                    .ToList();
            }

            return BuildTree(null);
        }

        public async Task SavePermissionsAsync(int groupId, List<PermissionAction> permissions)
        {
            var actionDict = await _context.Actions.ToDictionaryAsync(a => a.ActionName, a => a.ActionId);

            // Remove existing permissions for the group
            var existingPermissions = _context.GroupPermissions.Where(gp => gp.GroupId == groupId);
            _context.GroupPermissions.RemoveRange(existingPermissions);

            // Add new permissions
            var newPermissions = permissions
                .Where(p => actionDict.ContainsKey(p.Action))
                .Select(p => new GroupPermission
                {
                    GroupId = groupId,
                    MenuId = p.MenuId,
                    ActionId = actionDict[p.Action]
                });

            await _context.GroupPermissions.AddRangeAsync(newPermissions);
            await _context.SaveChangesAsync();
        }
    }

}
