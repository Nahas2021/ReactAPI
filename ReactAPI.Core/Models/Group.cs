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
}
