using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandlersServices.Model;

namespace HandlersServices
{
    public class UserHandler
    {
        #region [ Get Functions ]

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var users = context.User.ToList();

                    return users;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var user = await context.User.FindAsync(id);

                    return user;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<string> GetUserRolesById(Guid id)
        {
            try
            {
                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var user = await context.User.FindAsync(id);

                    if (user == null) return null;

                    return user.Roles;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<User> GetUserByNameAndPassword(string name, string password)
        {
            try
            {
                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var user = context.User.Where(u => u.Name.ToLower().Equals(name.ToLower()) && u.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        #endregion

        #region [ Update Functions ]

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var bdUser = await context.User.FindAsync(user.Id);
                    var userToReturn = new User();
                    if (bdUser != null)
                    {
                        context.Entry(bdUser).CurrentValues.SetValues(user);

                        await context.SaveChangesAsync();
                        userToReturn = await context.User.FindAsync(user.Id);
                    }
                    else
                    {
                        user = null;
                    }


                    return userToReturn;
                }

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        #endregion

        #region [ Add Functions ]

        public async Task<User> AddNewUser(User user)
        {
            try
            {

                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var bdUser = await context.User.FindAsync(user.Id);
                    var userToReturn = new User();

                    if (bdUser == null)
                    {
                        context.User.Add(user);
                        await context.SaveChangesAsync();
                        userToReturn = await context.User.FindAsync(user.Id);
                    }
                    else
                    {
                        userToReturn = null;
                    }

                    return userToReturn;
                }

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        #endregion

        #region [ Delete Functions ]

        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                using (var context = new UserDatabaseEntities1())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    var bdUser = await context.User.FindAsync(userId);

                    if (bdUser != null)
                    {
                        context.User.Remove(bdUser);
                        await context.SaveChangesAsync();
                    }

                    return true;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion
    }
}
