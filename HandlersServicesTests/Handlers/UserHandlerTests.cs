using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandlersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandlersServices.Model;
using System.IO;

namespace HandlersServices.Tests
{

    [TestClass()]
    public class UserHandlerTests
    {

        public UserHandlerTests()
        {            
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private UserHandler _userHandler = new UserHandler();

        [TestMethod()]
        public void GetAllUsersTest()
        {
            var users = _userHandler.GetAllUsers().Result;

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

            var user = _userHandler.GetUserById(userIdAdmin).Result;
            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void GetUserByIdTestWhenNull()
        {
            //Random Guid
            var randomGuid = Guid.NewGuid();

            var user = _userHandler.GetUserById(randomGuid).Result;
            Assert.IsNull(user);
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

            var user = _userHandler.GetUserRolesById(userIdPage3).Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void GetUserRolesByIdTestWhenNull()
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

            var user = _userHandler.GetUserRolesById(randomGuid).Result;

            Assert.IsNull(user);
        }

        [TestMethod()]
        public void GetUserByNameAndPasswordTest()
        {
            //user: PAGE2 password: PAGE2
            //user: PAGE1 password: PAGE1
            //user: PAGE3 password: PAGE3
            //user: ADMIN password: 123

            var user = _userHandler.GetUserByNameAndPassword("PAGE2", "PAGE2").Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void GetUserByNameAndPasswordTestWhenNull()
        {
            //user: PAGE2 password: PAGE2
            //user: PAGE1 password: PAGE1
            //user: PAGE3 password: PAGE3
            //user: ADMIN password: 123

            var user = _userHandler.GetUserByNameAndPassword("Inexistent User", "Random password").Result;

            Assert.IsNull(user);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {

            var userToUpdate = new User()
            {
                Id = new Guid("4f958265-0de4-4781-bda9-d69b3b48eaee"),
                Name = "PAGE1",
                Password = "123",
                Roles = "PAGE_1"
            };


            var user = _userHandler.UpdateUser(userToUpdate).Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void UpdateUserTestWhenNull()
        {
            var userToUpdate = new User()
            {
                Id = Guid.NewGuid(),
                Name = "PAGE1",
                Password = "123",
                Roles = "PAGE_1"
            };


            var user = _userHandler.UpdateUser(userToUpdate).Result;

            Assert.IsNull(user);
        }

        [TestMethod()]
        public void AddNewUserTest()
        {
            var userToInsert = new User()
            {
                Id = Guid.NewGuid(),
                Name = "new User",
                Password = "123",
                Roles = "PAGE_1;PAGE_2,PAGE_3"
            };


            var user = _userHandler.AddNewUser(userToInsert).Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void AddNewUserTestWhenNull()
        {
            var userToInsert = new User()
            {
                Id = new Guid("4f958265-0de4-4781-bda9-d69b3b48eaee"),
                Name = "Existing user",
                Password = "123",
                Roles = "PAGE_1;PAGE_2,PAGE_3"
            };


            var user = _userHandler.AddNewUser(userToInsert).Result;

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            var idPage1 = new Guid("4f958265-0de4-4781-bda9-d69b3b48eaee");

            bool success = _userHandler.DeleteUser(idPage1).Result;

            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void DeleteUserTestWhenNotExistUser()
        {
            bool success = _userHandler.DeleteUser(new Guid()).Result;

            Assert.IsFalse(success);
        }
    }
}