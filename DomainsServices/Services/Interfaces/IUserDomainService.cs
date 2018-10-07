using DomainsServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainsServices.Interfaces
{
    public interface IUserDomainService
    {
        /// <summary>
        /// Get all users from Handler
        /// </summary>
        /// <returns>Return list of users</returns>
        Task<List<UserDto>> GetAllUser();

        /// <summary>
        /// Get user by id user
        /// </summary>
        /// <param name="id">id from user</param>
        /// <returns>Return user serached or null</returns>
        Task<UserDto> GetUserById(Guid id);

        /// <summary>
        /// Get User by Name and Password
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>return the user searched or null</returns>
        Task<UserDto> GetUserByNameAndPassword(string name, string password);


        /// <summary>
        /// Add new user 
        /// </summary>
        /// <param name="userDtoToAdd">user will be added</param>
        /// <returns>returns user added or null</returns>
        Task<UserDto> AddNewUser(UserDto userDtoToAdd);

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="userDtoToUpdate">user with changes</param>
        /// <returns>return users updated or null</returns>
        Task<UserDto> UpdateUser(UserDto userDtoToUpdate);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="userIdToDelete">id from user to want to be deleted</param>
        /// <returns>returns bool that indicates if operation was success</returns>
        Task<bool> DeleteUser(Guid userIdToDelete);




    }
}
