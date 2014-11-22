using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDDSS_API.Repository;
using System.Web.Http.Cors;

namespace CDDSS_API.Controllers
{
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class UsersController : ApiController
    {
        private UsersRepository usersRep;

        public UsersController()
        {
            usersRep = new UsersRepository();
        }

        
        public List<UserShort> Get()
        {
            return usersRep.GetUsers();
        }

        [Route("api/users/detailed")]
        public List<User> GetDetailed()
        {
            return usersRep.GetUsersDetailed();
        }

        
    }
}
