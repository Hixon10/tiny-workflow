using System;
using System.Collections.Generic;
using Domain;
using Infrastructure;
using Infrastructure.Contracts;
using NUnit.Framework;
using Rhino.Mocks;
using Service;
using Service.Exceptions;

namespace ServiceTest
{
    [TestFixture]
    public class RoleServiceTest
    {
        [Test]
        public void GetRoles_Должны_вернуться_все_роли_в_системе()
        {
            // Arrange
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Description = "d",
                    Id = "1",
                    Name = "Director",
                    Priority = ApplicationRole.Priorities.First
                },
                new ApplicationRole
                {
                    Description = "d",
                    Id = "2",
                    Name = "Accountant",
                    Priority = ApplicationRole.Priorities.First
                },
                new ApplicationRole
                {
                    Description = "d",
                    Id = "2",
                    Name = "Employee",
                    Priority = ApplicationRole.Priorities.First
                },
                new ApplicationRole
                {
                    Description = "d",
                    Id = "2",
                    Name = "Admin",
                    Priority = ApplicationRole.Priorities.First
                }
            };

            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.Stub<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.ApplicationRoleRepository.GetAll()).Return(roles);

            RoleService roleService = new RoleService(unitOfWork);

            // Act
            mocks.ReplayAll();
            var result = roleService.GetRoles();

            //Assert
            CollectionAssert.AreEqual(result, roles);
        }

        [Test]
        [ExpectedException(typeof(BusinessLayerException), ExpectedMessage = "Нет ролей")]
        public void GetRoles_Нет_ролей_экспешнен()
        {
            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.Stub<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.ApplicationRoleRepository.GetAll()).Return(null);

            RoleService roleService = new RoleService(unitOfWork);

            // Act
            mocks.ReplayAll();
            var result = roleService.GetRoles();
        }

        [Test]
        public void GetRoleByName_Должна_вернуться_верная_роль()
        {
            // Arrange
            var role = new ApplicationRole
            {
                Description = "d",
                Id = "1",
                Name = "Director",
                Priority = ApplicationRole.Priorities.First
            };

            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.Stub<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.ApplicationRoleRepository.Get()).IgnoreArguments().Return(new List<ApplicationRole> {role});

            RoleService roleService = new RoleService(unitOfWork);

            // Act
            mocks.ReplayAll();
            var result = roleService.GetRoleByType(ApplicationRole.RoleTypes.Director);

            //Assert
            Assert.AreEqual(result, role);
        }

        [Test]
        [ExpectedException(typeof(BusinessLayerException), ExpectedMessage = "Роль не найдена")]
        public void GetRoleByName_Роль_Не_найдена()
        {
            // Arrange
            var role = new ApplicationRole
            {
                Description = "d",
                Id = "1",
                Name = "Director",
                Priority = ApplicationRole.Priorities.First
            };

            var mocks = new MockRepository();
            IUnitOfWork unitOfWork = mocks.Stub<IUnitOfWork>();
            unitOfWork.Stub(svc => svc.ApplicationRoleRepository.Get()).IgnoreArguments().Return(null);

            RoleService roleService = new RoleService(unitOfWork);

            // Act
            mocks.ReplayAll();
            var result = roleService.GetRoleByType(ApplicationRole.RoleTypes.Director);
        }

//        [Test]
//        public void ChangeRolesPriority_Приоритеты_должны_корректно_сохраниться()
//        {
//            var unitOfWork = new UnitOfWork(new ApplicationDbContext());
//            var roleService = new RoleService(unitOfWork);
//
//            var director = roleService.GetRoleByType(ApplicationRole.RoleTypes.Director);
//            var accountant = roleService.GetRoleByType(ApplicationRole.RoleTypes.Accountant);
//
//            var priorities = new Dictionary<ApplicationRole.RoleTypes, ApplicationRole.Priorities>
//            {
//                {ApplicationRole.RoleTypes.Director, ApplicationRole.Priorities.First},
//                {ApplicationRole.RoleTypes.Accountant, ApplicationRole.Priorities.Second}
//            };
//
//            roleService.ChangeRolesPriority(priorities);
//
//            var director2 = roleService.GetRoleByType(ApplicationRole.RoleTypes.Director);
//            var accountant2 = roleService.GetRoleByType(ApplicationRole.RoleTypes.Accountant);
//
//            Assert.AreEqual(director2, accountant);
//            Assert.AreEqual(accountant2, director);
//
//            /*
//            // Arrange
//            var priorities = new Dictionary <ApplicationRole.RoleTypes, ApplicationRole.Priorities>
//            {
//                {ApplicationRole.RoleTypes.Accountant, ApplicationRole.Priorities.First},
//                {ApplicationRole.RoleTypes.Accountant, ApplicationRole.Priorities.NotApprove}
//            };
//
//            var mocks = new MockRepository();
//            IUnitOfWork unitOfWork = mocks.Stub<IUnitOfWork>();
//            unitOfWork.Stub(svc => svc.ApplicationRoleRepository.Get(p => p.Name == priorities[0].ToString())).Return(new List<ApplicationRole> { priorities[0] });
//
//            RoleService roleService = new RoleService(unitOfWork);
//
//            // Act
//            mocks.ReplayAll();
//            var result = roleService.GetRoleByType(ApplicationRole.RoleTypes.Director);
//
//            //Assert
//            Assert.AreEqual(result, role);
//             * */
//        }
    }
}
