using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client
{
    /// <summary>
    /// Singleton pattern for UserSession
    /// </summary>
    public class SessionManager
    {
        private static Dictionary<string, UserSession> userSessions;

        /// <summary>
        /// returns UserSession by sessionID
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// adds a new UserSession object by sessionID
        /// </summary>
        /// <param name="sessionID"></param>
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

        /// <summary>
        /// removes a UserSession by sessionID
        /// called when logging out
        /// </summary>
        /// <param name="sessionID"></param>
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