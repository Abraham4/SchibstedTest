using DomainsServices.Entities;
using DomainsServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using ApiUserManagement.Filters;
using WebApplicationTest.Filters;

namespace WebApplicationTest.Controllers
{
    [BasicAuthentication]
    public class UserController : ApiController
    {
        private IUserDomainService _userDomainService;
        public UserController(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        [HttpPost]
        [ActionName("GetUserById")]
        public async Task<HttpResponseMessage> GetUser(Guid userId)
        {
            try
            {
                var user = await _userDomainService.GetUserById(userId);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new ObjectContent<UserDto>(user, new JsonMediaTypeFormatter(), "application/json") };
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("AuthenticateUser")]
        public async Task<HttpResponseMessage> AuthenticateUser([FromBody]Models.UserInfo userInfo)
        {
            try
            {
                var user = await _userDomainService.GetUserByNameAndPassword(userInfo.Name,userInfo.Password);

                if (user == null) return new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new ObjectContent<UserDto>(user, new JsonMediaTypeFormatter(), "application/json") };
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new ObjectContent<UserDto>(user, new JsonMediaTypeFormatter(), "application/json") };
                }
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

    }
}
