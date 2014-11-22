using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDDSS_API.Repository
{
    public class UsersRepository : RepositoryBase
    {

        public List<UserShort> GetUsers() 
        {
            var query = from user in ctx.Users
                        select new
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        };
            List<UserShort> list = new List<UserShort>();
            foreach (var u in query)
            {
                list.Add(new UserShort(u.FirstName, u.LastName));
            }
            return list;
        }

        public List<User> GetUsersDetailed()
        {
            return ctx.Users.ToList();
        }
    }
}