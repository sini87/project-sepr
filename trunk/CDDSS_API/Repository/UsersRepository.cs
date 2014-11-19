using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDDSS_API.Repository
{
    public class UsersRepository
    {
        private IObjectContextFactory _objectContextFactory;

        public UsersRepository()
        {
            _objectContextFactory = new LazySingletonObjectContextFactory();
        }

        public List<UserShort> GetUsers()
        {
            var query = from user in _objectContextFactory.Create().Users
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
            return _objectContextFactory.Create().Users.ToList();
        }
    }
}