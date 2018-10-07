using DomainsServices.Entities;
using DomainsServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandlersServices;
using HandlersServices.Handlers;
using HandlersServices.Model;

namespace DomainsServices
{
    public class UserDomainService : IUserDomainService
    {

        #region [ Public Functions ]

        #region [Get Functions ]

        public async Task<List<UserDto>> GetAllUser()
        {
            var users = await HandlersManager.Instance.UserHandler.GetAllUsers();

            if (users == null) return null;

            var usersDto = await ConvertUserToDto(users);

            return usersDto;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await HandlersManager.Instance.UserHandler.GetUserById(id);

            if (user == null) return null;

            var userDto = await ConvertUserToDto(user);

            return userDto;

        }

        public async Task<UserDto> GetUserByNameAndPassword(string name, string password)
        {
            var user = await HandlersManager.Instance.UserHandler.GetUserByNameAndPassword(name, password);

            if (user == null) return null;
            var userDto = await ConvertUserToDto(user);

            return userDto;
        }

        public async Task<List<string>> GetUserRolesById(Guid id)
        {
            var roles = await HandlersManager.Instance.UserHandler.GetUserRolesById(id);

            return roles.Split(';').ToList();
        }

        #endregion

        #region [ Add Functions ]

        public async Task<UserDto> AddNewUser(UserDto userDtoToAdd)
        {
            var user = await ConvertDtoToUser(userDtoToAdd);

            var userAdded = await HandlersManager.Instance.UserHandler.AddNewUser(user);

            if (userAdded == null) return null;

            var userDtoAdded = await ConvertUserToDto(userAdded);

            return userDtoAdded;
        }

        #endregion

        #region [ Update Functions ]

        public async Task<UserDto> UpdateUser(UserDto userDtoToUpdate)
        {
            try
            {
                var userConverted = await ConvertDtoToUser(userDtoToUpdate);

                var userUpdated = await HandlersManager.Instance.UserHandler.UpdateUser(userConverted);

                if (userUpdated == null) return null;

                var userDtoUpdated = await ConvertUserToDto(userUpdated);

                return userDtoUpdated;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        #endregion

        #region [ Delete Functions ]

        public async Task<bool> DeleteUser(Guid userIdToDelete)
        {
            try
            {

                var successDelete = await HandlersManager.Instance.UserHandler.DeleteUser(userIdToDelete);

                return successDelete;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #endregion

        #region [ Private Functions ]

        private async Task<List<UserDto>> ConvertUserToDto(List<User> users)
        {
            try
            {
                List<UserDto> usersDto = users.Select(u => new UserDto()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Password = u.Password,
                    Roles = u.Roles
                })
                .ToList();

                return usersDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<UserDto> ConvertUserToDto(User user)
        {
            try
            {
                UserDto userDto = new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    Roles = user.Roles
                };

                return userDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<User> ConvertDtoToUser(UserDto userDto)
        {
            try
            {
                var user = new User()
                {
                    Id = userDto.Id,
                    Name = userDto.Name,
                    Password = userDto.Password,
                    Roles = userDto.Roles
                };

                return user;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
    }
}
