namespace ReactAPI.Core.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public int? ParentMenuId { get; set; }
        public string? Route { get; set; }
        public Menu? ParentMenu { get; set; }
        public ICollection<Menu> Children { get; set; } = new List<Menu>();
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }

}
