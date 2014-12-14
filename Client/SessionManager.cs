using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client
{
    public class SessionManager
    {
        private static Dictionary<string, UserSession> userSessions;

        public static UserSession GetUserSession(string sessionID)
        {
            if (userSessions == null)
            {
                userSessions = new Dictionary<string, UserSession>();
                return null;
            }else if(userSessions.ContainsKey(sessionID)){
                return userSessions[sessionID];
            }
            return null;
        }

        public static void AddUserSession(string sessionID)
        {
            if (userSessions == null)
            {
                userSessions = new Dictionary<string, UserSession>();;
            };
            if (!userSessions.ContainsKey(sessionID))
            {
                userSessions.Add(sessionID, new UserSession());
            }
        }

        public static void RemoveUserSession(string sessionID)
        {
            if (userSessions == null)
            {
                userSessions = new Dictionary<string, UserSession>();
            }
            if (userSessions.ContainsKey(sessionID))
            {
                userSessions.Remove(sessionID);
            }
        }
    }
}