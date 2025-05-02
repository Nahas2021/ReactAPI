namespace ReactAPI.Core.Models
{
    public class PermissionTreeNode
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public string? Route { get; set; }
        public List<string> Actions { get; set; } = new();
        public List<PermissionTreeNode> Children { get; set; } = new();
    }
}


