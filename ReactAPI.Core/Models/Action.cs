namespace ReactAPI.Core.Models
{
    public class Action
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }

}
