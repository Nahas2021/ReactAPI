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
        public async Task<List<PermissionActions>> GetPermissions(int groupId)
        {
            var result = from g in _context.GroupPermissions
                         join a in _context.Actions on g.ActionId equals a.ActionId into actionGroup
                         from a in actionGroup.DefaultIfEmpty()
                         select new PermissionActions
                         {
                             GroupId=g.GroupId,
                             MenuId=g.MenuId,
                             ActionId = g.ActionId,  // Explicitly from GroupPermissions
                             ActionName = a.ActionName // From Actions (nullable due to left join)
                         };

             return (List<PermissionActions>)result;
        }
        public async Task<List<PermissionActions>> GetPermissionTreeAsync(int groupId)
        {
           
                var query = _context.GroupPermissions
                .Where(g => g.GroupId == groupId)  // Filter added here
                    .GroupJoin(
                        _context.Actions,
                        g => g.ActionId,
                        a => a.ActionId,
                        (g, actionGroup) => new { g, actionGroup }
                    )
                    .SelectMany(
                        x => x.actionGroup.DefaultIfEmpty(),
                        (x, a) => new PermissionActions
                        {
                            GroupId = x.g.GroupId,
                            MenuId = x.g.MenuId,
                            ActionId = x.g.ActionId,
                            ActionName =a.ActionName
                        }
                    );

                return await query.ToListAsync();
          
        }
        public async Task<List<PermissionTreeNode>> GetPermissionTree(int groupId)
        {
            var menus = await _context.Menus.ToListAsync();
            var actions = await _context.Actions.ToListAsync();
            var groupPermissions = await _context.GroupPermissions
                //.Where(gp => gp.GroupId == groupId)
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

        public async Task<List<MenuActionDto>> GetMenuTreeAsync()
        {
            var menus = await _context.Menus.ToListAsync();
            var actions = await _context.Actions.ToListAsync(); // Optional: link to specific menus in real case

            var menuDtos = menus.Select(m => new MenuActionDto
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName,
                ParentMenuId = m.ParentMenuId,
                Route = m.Route,
                Actions = actions.Select(a => new ActionDto
                {
                    ActionId = a.ActionId,
                    ActionName = a.ActionName
                }).ToList()
                //Actions = actions.Select(a => a.ActionName).ToList() // Demo: attach all actions to all menus
            }).ToList();

            return BuildTree(menuDtos, null);
        }

        private List<MenuActionDto> BuildTree(List<MenuActionDto> allMenus, int? parentId)
        {
            return allMenus
                .Where(m => m.ParentMenuId == parentId)
                .Select(m => new MenuActionDto
                {
                    MenuId = m.MenuId,
                    MenuName = m.MenuName,
                    ParentMenuId = m.ParentMenuId,
                    Route = m.Route,
                    Actions = m.Actions,
                    Children = BuildTree(allMenus, m.MenuId)
                }).ToList();
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
