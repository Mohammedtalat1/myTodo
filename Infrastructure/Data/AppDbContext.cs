using TODO.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TODO.Infrastructure.Data
{
    /// <summary>
    /// Application database context. Configures all DbSets and Fluent API relationships.
    /// All entity configurations follow DIP: domain entities have no EF Core dependency.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<WorkItems> WorkItems { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Boards> Boards { get; set; }
        public DbSet<BoardColumns> BoardColumns { get; set; }
        public DbSet<Attachments> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ── RolePermission: composite key (no Id needed) ──────────────────────────
            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);

            // ── ProjectMember: composite unique key prevents duplicate memberships ────
            builder.Entity<ProjectMember>()
                .HasIndex(pm => new { pm.UserId, pm.ProjectId })
                .IsUnique();

            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMemberships)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.Members)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── WorkItems: self-referencing hierarchy (Epic → Feature → Task) ─────────
            builder.Entity<WorkItems>()
                .HasOne(w => w.Parent)
                .WithMany(w => w.Children)
                .HasForeignKey(w => w.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade-deleting entire tree

            // ── WorkItems → AssignedUser: deleting user nulls out assignments ─────────
            builder.Entity<WorkItems>()
                .HasOne(w => w.AssignedUser)
                .WithMany()
                .HasForeignKey(w => w.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ── Comments: cascade-delete comments when work item is deleted ───────────
            builder.Entity<Comments>()
                .HasOne(c => c.Task)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Attachments: cascade-delete attachments when work item is deleted ──────
            builder.Entity<Attachments>()
                .HasOne(a => a.WorkItem)
                .WithMany(w => w.Attachments)
                .HasForeignKey(a => a.WorkItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
