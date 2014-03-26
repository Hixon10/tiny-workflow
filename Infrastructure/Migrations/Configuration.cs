using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //C������� ����� ��������
            context.Accounts.AddOrUpdate(new[] {new Account {Balance = 0}});

            //�������� ��������� ������������
            context.ProductCategories.AddOrUpdate(new[] { new ProductCategory { Name = "�����", } });
            context.ProductCategories.AddOrUpdate(new[] { new ProductCategory { Name = "���" } });
            context.ProductCategories.AddOrUpdate(new[] { new ProductCategory { Name = "����������" } });
            context.ProductCategories.AddOrUpdate(new[] { new ProductCategory { Name = "���������" } });
            context.ProductCategories.AddOrUpdate(new[] { new ProductCategory { Name = "����������" } });

            //�������� �������� ����
            context.ApplicationLogStatuses.AddOrUpdate(new[] { new ApplicationLogStatus { Name = "Created", Description = "�������" } });
            context.ApplicationLogStatuses.AddOrUpdate(new[] { new ApplicationLogStatus { Name = "Paid", Description = "��������"} });
            context.ApplicationLogStatuses.AddOrUpdate(new[] { new ApplicationLogStatus { Name = "Denied", Description = "��������" } });
            context.ApplicationLogStatuses.AddOrUpdate(new[] { new ApplicationLogStatus { Name = "UnderApproval", Description = "�� ������ ���������" } });
            context.ApplicationLogStatuses.AddOrUpdate(new[] { new ApplicationLogStatus { Name = "Approved", Description = "��������" } });

            //�������� ����� �������
            context.Roles.AddOrUpdate(new[]
            {
                new ApplicationRole
                {
                    Name = "Director",
                    Description = "��������",
                    Priority = ApplicationRole.Priorities.Second
                }
            });
            context.Roles.AddOrUpdate(new[]
            {
                new ApplicationRole
                {
                    Name = "Accountant",
                    Description = "���������",
                    Priority = ApplicationRole.Priorities.First
                }
            });
            context.Roles.AddOrUpdate(new[]
            {
                new ApplicationRole
                {
                    Name = "Employee",
                    Description = "���������",
                    Priority = ApplicationRole.Priorities.NotApprove
                }
            });
            context.Roles.AddOrUpdate(new[]
            {
                new ApplicationRole
                {
                    Name = "Admin",
                    Description = "Admin",
                    Priority = ApplicationRole.Priorities.NotApprove
                }
            });

            //�������� �������������
            var userManager =
                    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            CreateUser(userManager, "admin", "password", ApplicationRole.RoleTypes.Admin);

            CreateUser(userManager, "user", "password", ApplicationRole.RoleTypes.Employee);
            CreateUser(userManager, "user2", "password", ApplicationRole.RoleTypes.Employee);
            CreateUser(userManager, "user3", "password", ApplicationRole.RoleTypes.Employee);

            CreateUser(userManager, "director", "password", ApplicationRole.RoleTypes.Director);
            CreateUser(userManager, "director2", "password", ApplicationRole.RoleTypes.Director);
            CreateUser(userManager, "director3", "password", ApplicationRole.RoleTypes.Director);

            CreateUser(userManager, "accountant", "password", ApplicationRole.RoleTypes.Accountant);
            CreateUser(userManager, "accountant2", "password", ApplicationRole.RoleTypes.Accountant);
            CreateUser(userManager, "accountant3", "password", ApplicationRole.RoleTypes.Accountant);
        }

        private void CreateUser(UserManager<ApplicationUser> um, string login, string password,
            ApplicationRole.RoleTypes role)
        {
            var userId = Guid.NewGuid().ToString();

            var user = new ApplicationUser
            {
                UserName = login,
                Id = userId
            };

            um.Create(user, password);

            um.AddToRole(user.Id, role.ToString());
        }
    }
}
