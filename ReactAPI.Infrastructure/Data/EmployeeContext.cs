using ReactAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Action = ReactAPI.Core.Models.Action;

namespace ReactAPI.Infrastructure.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupId, gp.MenuId, gp.ActionId });

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Group)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(gp => gp.GroupId);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Menu)
                .WithMany(m => m.GroupPermissions)
                .HasForeignKey(gp => gp.MenuId);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Action)
                .WithMany(a => a.GroupPermissions)
                .HasForeignKey(gp => gp.ActionId);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.ParentMenu)
                .WithMany(m => m.Children)
                .HasForeignKey(m => m.ParentMenuId);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Action> Actions { get; set; } = null!;
        public DbSet<GroupPermission> GroupPermissions { get; set; } = null!;
    }
}