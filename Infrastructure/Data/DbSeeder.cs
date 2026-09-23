using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO.Domain.Entities;

namespace TODO.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(DbSeeder));

            try
            {
                // 1. Seed Roles
                if (!await context.Roles.AnyAsync())
                {
                    logger.LogInformation("Seeding default roles...");
                    var defaultRoles = new List<Roles>
                    {
                        new() { Name_En = "Admin", Name_Ar = "مدير", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Developer", Name_Ar = "مطور", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Manager", Name_Ar = "مشرف", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Member", Name_Ar = "عضو", CreatedAt = DateTime.UtcNow }
                    };

                    await context.Roles.AddRangeAsync(defaultRoles);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Default roles seeded successfully.");
                }

                // 2. Seed Permissions
                if (!await context.Permissions.AnyAsync())
                {
                    logger.LogInformation("Seeding default permissions...");
                    var permissions = new List<Permissions>
                    {
                        new() { Name_En = "Projects.View", Name_Ar = "عرض المشاريع", Description = "Can view projects", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Projects.Create", Name_Ar = "إنشاء المشاريع", Description = "Can create projects", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Projects.Edit", Name_Ar = "تعديل المشاريع", Description = "Can edit projects", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Projects.Delete", Name_Ar = "حذف المشاريع", Description = "Can delete projects", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Tasks.View", Name_Ar = "عرض المهام", Description = "Can view work items", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Tasks.Create", Name_Ar = "إنشاء المهام", Description = "Can create work items", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Tasks.Edit", Name_Ar = "تعديل المهام", Description = "Can edit work items", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Tasks.Delete", Name_Ar = "حذف المهام", Description = "Can delete work items", CreatedAt = DateTime.UtcNow },
                        new() { Name_En = "Users.Manage", Name_Ar = "إدارة المستخدمين", Description = "Can manage system users", CreatedAt = DateTime.UtcNow }
                    };

                    await context.Permissions.AddRangeAsync(permissions);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Default permissions seeded successfully.");
                }

                // 3. Seed RolePermission mappings
                if (!await context.RolePermissions.AnyAsync())
                {
                    logger.LogInformation("Seeding role permissions...");
                    var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name_En == "Admin");
                    var devRole = await context.Roles.FirstOrDefaultAsync(r => r.Name_En == "Developer");
                    var allPermissions = await context.Permissions.ToListAsync();

                    var rolePermissions = new List<RolePermission>();

                    if (adminRole != null)
                    {
                        // Admin gets all permissions
                        rolePermissions.AddRange(allPermissions.Select(p => new RolePermission
                        {
                            RoleId = adminRole.Id,
                            PermissionId = p.Id
                        }));
                    }

                    if (devRole != null)
                    {
                        // Developer gets project and task permissions
                        rolePermissions.AddRange(allPermissions
                            .Where(p => p.Name_En != null && (p.Name_En.StartsWith("Projects.") || p.Name_En.StartsWith("Tasks.")))
                            .Select(p => new RolePermission
                            {
                                RoleId = devRole.Id,
                                PermissionId = p.Id
                            }));
                    }

                    if (rolePermissions.Count > 0)
                    {
                        await context.RolePermissions.AddRangeAsync(rolePermissions);
                        await context.SaveChangesAsync();
                        logger.LogInformation("Role permissions seeded successfully.");
                    }
                }

                // 4. Seed Default Admin User
                var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@todo.com");
                if (adminUser == null)
                {
                    var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name_En == "Admin");
                    if (adminRole != null)
                    {
                        logger.LogInformation("Seeding default admin user...");
                        var defaultAdmin = new Users
                        {
                            FullName = "System Administrator",
                            Username = "admin",
                            Email = "admin@todo.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("Admin@123456"),
                            RoleId = adminRole.Id,
                            CreatedAt = DateTime.UtcNow
                        };

                        await context.Users.AddAsync(defaultAdmin);
                        await context.SaveChangesAsync();
                        logger.LogInformation("Default admin user created successfully (Email: admin@todo.com).");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
