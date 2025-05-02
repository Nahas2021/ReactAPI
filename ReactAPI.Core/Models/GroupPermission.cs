namespace ReactAPI.Core.Models
{
    public class GroupPermission
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;
        public int ActionId { get; set; }
        public Action Action { get; set; } = null!;
    }

}
