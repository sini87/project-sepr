﻿using CDDSS_API.Models.Domain;
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
            DataClassesDataContext ctx = new DataClassesDataContext();
            var query = from user in ctx.Users
                        select new
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            AO = (int)user.AccessObject,
                            Email = user.Email
                        };
            List<UserShort> list = new List<UserShort>();
            foreach (var u in query)
            {
                list.Add(new UserShort(u.FirstName, u.LastName, u.AO, u.Email));
            }
            return list;
        }

        /// <summary>
        /// return a UserShort by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserShort GetUserByEmail(string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            var query = from user in ctx.Users where user.Email == email
                        select new
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            AO = (int)user.AccessObject,
                            Email = user.Email
                        };
            return new UserShort(query.First().FirstName, query.First().LastName, query.First().AO, query.First().Email);
        }

        /// <summary>
        /// Returns user detailed
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserShort GetUserDetailByEmail(string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            var query = from user in ctx.Users
                        where user.Email == email
                        select new
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            //AO = (int)user.AccessObject,
                            Email = user.Email,
                            PasswordHash = user.PasswordHash,
                            //PhoneNumber = user.PhoneNumber,
                            UserName=user.UserName,
                            SecretQuestion=user.SecretQuestion,
                            Answer=user.Answer
                        };
            UserShort usershort = new UserShort(query.First().Email, query.First().FirstName, query.First().LastName, query.First().UserName, query.First().SecretQuestion, query.First().Answer, query.First().PasswordHash);
            return usershort;
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
        public bool EditUser(UserShort user, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            try
            {
                User u = ctx.Users.First(x => x.Email == email);
                if (user.FirstName != "" && user.FirstName != null) u.FirstName = user.FirstName;
                if (user.LastName != "" && user.LastName != null) u.LastName = user.LastName;
                if (user.SecretQuestion!="" && user.SecretQuestion != null) u.SecretQuestion = user.SecretQuestion;
                if (user.Answer!="" && user.Answer != null) u.Answer = user.Answer;
                if (user.Email != "" && user.Email != null)
                {
                    u.Email = user.Email;
                    u.UserName = user.Email;
                }
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
            DataClassesDataContext ctx = new DataClassesDataContext();
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
            AccessObject ao;
            if (query.ToList().Count() > 0){
                ao = new AccessObject()
                {
                    Id = query.ToList()[0].MaxId + 1
                };
            }
            else
            {
                ao = new AccessObject()
                {
                    Id = 1
                };
            }
            
            ctx.AccessObjects.InsertOnSubmit(ao);
            User u = ctx.Users.First(x => x.Email == email);
            u.AccessObject = ao.Id;
            ctx.SubmitChanges();
        }
    }
}