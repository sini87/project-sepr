using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDDSS_API.Repository;
using System.Web.Http.Cors;
using System.Web;

namespace CDDSS_API.Controllers
{
    /// <summary>
    /// ApiController User
    /// used retrieving list of users and editing user
    /// </summary>
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class UserController : ApiController
    {
        private UserRepository usersRep;

        /// <summary>
        /// default constructer
        /// </summary>
        public UserController()
        {
            usersRep = new UserRepository();
        }

        
        /// <summary>
        /// returns a list with all users (only first name and last name)
        /// </summary>
        /// <returns></returns>
        public List<UserShort> Get()
        {
            return usersRep.GetUsers();
        }

        /// <summary>
        /// returns current logged in user
        /// </summary>
        /// <returns>UserShort</returns>
        [Route("api/User/Current")]
        public UserShort GET()
        {
            return usersRep.GetUserByEmail(RequestContext.Principal.Identity.Name);
        }

        /// <summary>
        /// edits a user profile
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="secretQuestion"></param>
        /// <param name="answer"></param>
        /// <returns>returns HTTP Status Code OK if operation was successful, else HTTP Status Cude NOT Acceptable</returns>
        public HttpResponseMessage Post(string firstName="", string lastName = "", string secretQuestion = "", string answer = "")
        {
            HttpResponseMessage result = null;
            string email = HttpContext.Current.User.Identity.Name;

            if (email != null && email.Length > 0 && usersRep.EditUser(email, firstName, lastName, secretQuestion, answer))
            {

                result = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            return result;
        }

        
    }
}
