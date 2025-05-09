namespace ReactAPI.Core.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }

    public class UserGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        
    }
    public class MenuActionDto
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string? Route { get; set; }
        public int? ParentMenuId { get; set; }
        public List<MenuActionDto>? Children { get; set; }
        public List<ActionDto>? Actions { get; set; }
    }
    public class PermissionActions
    {
        public int MenuId { get; set; }
        public string ActionName { get; set; }
        public int GroupId { get; set; }
        public int? ActionId { get; set; }
    }
    public class ActionDto
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
    }

}
