using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainsServices.Entities;
using System.IO;

namespace DomainsServices.Tests
{
    [TestClass()]
    public class UserDomainServiceTests
    {
        private UserDomainService _userDomainService = new UserDomainService();

        public UserDomainServiceTests()
        {
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        [TestMethod()]
        public void GetAllUserTest()
        {

            var users = _userDomainService.GetAllUser().Result;

            Assert.IsNotNull(users);
        }

        [TestMethod()]
        public void GetUserByIdTest()
        {
            //PAGE1 user
            var userIdPage1 = new Guid("4f958265-0de4-4781-bda9-d69b3b48eaee");

            //PAGE2 user
            var userIdPage2 = new Guid("e99516ff-e3c0-46c3-8cc1-1fcc108e4056");

            //PAGE3 user
            var userIdPage3 = new Guid("548d1d3f-49cb-423f-b31a-93ff3a2943e5");

            //Admin user
            var userIdAdmin = new Guid("1f4b5a0e-618f-44f2-af49-9d1cb9abec9f");

            //Random Guid
            var randomGuid = Guid.NewGuid();

            var user = _userDomainService.GetUserById(userIdPage1).Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void GetUserByNameAndPasswordTest()
        {
            //user: PAGE2 password: PAGE2
            //user: PAGE1 password: PAGE1
            //user: PAGE3 password: PAGE3
            //user: ADMIN password: 123
            var user = _userDomainService.GetUserByNameAndPassword("PAGE1", "PAGE1").Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void GetUserRolesByIdTest()
        {
            //PAGE1 user
            var userIdPage1 = new Guid("4f958265-0de4-4781-bda9-d69b3b48eaee");

            //PAGE2 user
            var userIdPage2 = new Guid("e99516ff-e3c0-46c3-8cc1-1fcc108e4056");

            //PAGE3 user
            var userIdPage3 = new Guid("548d1d3f-49cb-423f-b31a-93ff3a2943e5");

            //Admin user
            var userIdAdmin = new Guid("1f4b5a0e-618f-44f2-af49-9d1cb9abec9f");

            var user = _userDomainService.GetUserRolesById(userIdAdmin).Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void AddNewUserTest()
        {
            var user = new UserDto()
            {
                Id = Guid.NewGuid(),
                Name = "TestUser",
                Password = "TestUserPassword",
                Roles = "PAGE_1;PAGE_2;PAGE_3;Admin"
            };
            var userAdded = _userDomainService.AddNewUser(user).Result;
            Assert.IsNotNull(userAdded);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            var user = new UserDto()
            {
                Id = new Guid("e99516ff-e3c0-46c3-8cc1-1fcc108e4056"),
                Name = "Page2User",
                Password = "PAGE2",
                Roles = "PAGE_2"
            };
            var userUpdated = _userDomainService.UpdateUser(user).Result;

            Assert.IsNotNull(userUpdated);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            //PAGE2 user
            var userIdPage2 = new Guid("e99516ff-e3c0-46c3-8cc1-1fcc108e4056");

            var deleted = _userDomainService.DeleteUser(userIdPage2).Result;

            Assert.IsTrue(deleted);
        }
    }
}