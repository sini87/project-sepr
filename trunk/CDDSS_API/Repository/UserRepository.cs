using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// makes db operations on table user
    /// </summary>
    public class UserRepository : RepositoryBase
    {

        /// <summary>
        /// returns a list with all users
        /// </summary>
        /// <returns></returns>
        public List<UserShort> GetUsers() 
        {
            var query = from user in ctx.Users
                        select new
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            AO = (int)user.AccessObject
                        };
            List<UserShort> list = new List<UserShort>();
            foreach (var u in query)
            {
                list.Add(new UserShort(u.FirstName, u.LastName, u.AO));
            }
            return list;
        }

        /// <summary>
        /// edits a user
        /// </summary>
        /// <param name="email">is necessary</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="secretQuestion"></param>
        /// <param name="answer"></param>
        /// <returns>true if update is successful</returns>
        public bool EditUser(string email, string firstName, string lastName, string secretQuestion, string answer)
        {
            try
            {
                User u = ctx.Users.First(x => x.Email == email);
                u.FirstName = firstName;
                u.LastName = lastName;
                u.SecretQuestion = secretQuestion;
                u.Answer = answer;
                ctx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// creates an accessobject for the user
        /// </summary>
        /// <param name="email"></param>
        public void CreateAccessObjectForUser(string email)
        {
            var query = from AccessObjects in
                            (from AccessObjects in ctx.AccessObjects
                             select new
                             {
                                 AccessObjects.Id,
                                 Dummy = "x"
                             })
                        group AccessObjects by new { AccessObjects.Dummy } into g
                        select new
                        {
                            MaxId = (int)g.Max(p => p.Id)
                        };
            AccessObject ao = new AccessObject()
            {
                Id = query.ToList()[0].MaxId + 1
            };
            ctx.AccessObjects.InsertOnSubmit(ao);
            User u = ctx.Users.First(x => x.Email == email);
            u.AccessObject = ao.Id;
            ctx.SubmitChanges();
        }
    }
}