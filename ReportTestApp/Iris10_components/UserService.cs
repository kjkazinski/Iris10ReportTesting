using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

using IrisWeb.Code.Data.Models.API;
using IrisWeb.Code.Data.Models.Database;
using System.Data;
using System.Data.SqlClient;
using IrisWeb.Code.Data.Helpers;

namespace IrisWeb.Code.Data.Services
{
    public sealed class UserService : DatabaseServiceBase<UsersModel>
    {
        private const string C_SESSION_USER_INFO_KEY = "IRIS_USER_INFO";
        private const int C_SESSION_LIFESPAN = 15 * 60;
        private const int C_SALT_VALUE_SIZE = 16;

        public bool IsAuthenticated()
        {
            return HttpContext.Current.Items[C_SESSION_USER_INFO_KEY] != null;
        }

        public void SetUserInformationForCurrentRequest(AuthUserInformationModel userInfo)
        {
            HttpContext.Current.Items[C_SESSION_USER_INFO_KEY] = userInfo;
        }

        public AuthUserInformationModel GetUserInformationForCurrentRequest()
        {
            return (AuthUserInformationModel)HttpContext.Current.Items[C_SESSION_USER_INFO_KEY];
        }

        public bool ValidateSecurityLevel(string viewName, int requiredMinLevel)
        {
            AuthUserInformationModel userInfo = GetUserInformationForCurrentRequest();
            if (userInfo != null && userInfo.RoleLookup.ContainsKey(viewName))
            {
                if (userInfo.RoleLookup[viewName] >= requiredMinLevel)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Starts a new session for the user by validating their username and password
        /// credentials.
        /// </summary>
        public bool StartSession(string username, string password, out string token, out DateTime expiration)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "User");
            sqlGen.AddField("*");
            sqlGen.AddWhereParameter("User", "UserName", username, SqlWhereComparison.SqlComparer.Equal);

            System.Data.DataTable dt = Adocls.FetchDataTable(sqlGen, modelDataBindings.DatabaseName);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HashPassword"].ToString() == CryptoHelper.ComputeHash(password, dt.Rows[0]["SALT"].ToString()))
                {
                    string sessionKey = Guid.NewGuid().ToString("N");
                    DateTime sessionExpires = DateTime.Now.AddMinutes(C_SESSION_LIFESPAN);

                    SqlGenerator sqlGenUpdate = new SqlGenerator(SqlGenerator.SqlTypes.Update, "User");
                    sqlGenUpdate.AddField("AuthGUID", "User", sessionKey);
                    sqlGenUpdate.AddField("AuthDate", "User", sessionExpires);
                    sqlGenUpdate.AddWhereParameter("User", "User_Key", dt.Rows[0]["User_Key"].ToString(), SqlWhereComparison.SqlComparer.Equal);

                    Adocls.ExecuteSql(sqlGenUpdate, true, modelDataBindings.DatabaseName);

                    token = sessionKey;
                    expiration = sessionExpires;

                    return true;
                }
            }

            token = null;
            expiration = DateTime.MinValue;
            return false;
        }

        /// <summary>
        /// Starts a new session for the user and creates a new HttpCookie object with the 
        /// new session information for the user.
        /// </summary>
        public HttpCookie StartSessionCookie(string username, string password)
        {
            string newSessionCode;
            DateTime newSessionExpires;

            if (StartSession(username, password, out newSessionCode, out newSessionExpires))
            {
                HttpCookie cookie = new HttpCookie("IRIS_SESSION", newSessionCode);
                cookie.HttpOnly = true;
                cookie.Expires = newSessionExpires;

                return cookie;
            }

            return null;
        }

        /// <summary>
        /// Validate a session GUID string with the database to make sure a session exists
        /// for this GUID.s
        /// </summary>
        public bool ValidateSessionKey(string sessionGuid)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "User");
            sqlGen.AddField("User_Key");
            sqlGen.AddWhereParameter("User", "AuthGUID", sessionGuid, SqlWhereComparison.SqlComparer.Equal);

            return Adocls.FetchValueString(sqlGen, "UserDatabase").ToString().Length > 0;
        }

        /// <summary>
        /// Get AuthUserInformationModel using the session GUID string
        /// </summary>
        public AuthUserInformationModel GetAuthUserInformation(string sessionGuid)
        {
            try
            {
                SqlGenerator sqlGenUser = new SqlGenerator(SqlGenerator.SqlTypes.Select, "User");
                sqlGenUser.AddField("*");
                sqlGenUser.AddWhereParameter("User", "AuthGUID", sessionGuid, SqlWhereComparison.SqlComparer.Equal);
                sqlGenUser.AddWhereParameter("User", "AuthDate", DateTime.Now, SqlWhereComparison.SqlComparer.GreaterThan | SqlWhereComparison.SqlComparer.Equal);

                // TODO: Optimize this to use reader instead of data table
                DataTable dt = Adocls.FetchDataTable(sqlGenUser, "UserDatabase");

                return BuildAuthUserInformationModel(dt);
            }
            catch { return null; }
        }

        /// <summary>
        /// Get AuthUserInformationmodel using the email and active state of the user account
        /// </summary>
        public AuthUserInformationModel GetAuthUserInformation(string email, bool activeFlag)
        {
            try
            {
                SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "User");
                sqlGen.AddField("*");
                sqlGen.AddWhereParameter("User", "UserName", email, SqlWhereComparison.SqlComparer.Equal);
                sqlGen.AddWhereParameter("User", "Active", activeFlag.ToString(), SqlWhereComparison.SqlComparer.Equal);

                // TODO: Should transition this to a data reader
                DataTable dt = Adocls.FetchDataTable(sqlGen, "UserDatabase");

                return BuildAuthUserInformationModel(dt);
            }
            catch (Exception)
            {

            }

            return null;

        }

        private AuthUserInformationModel BuildAuthUserInformationModel(DataTable userInfoTable)
        {
            // Get basic user information from the databases
            AuthUserInformationModel userInfo = new AuthUserInformationModel();
            userInfo.UserKey = userInfoTable.Rows[0]["User_Key"].ToString();
            userInfo.OldUserKey = userInfoTable.Rows[0]["SecurityUser_Key"].ToString();
            userInfo.Username = userInfoTable.Rows[0]["UserName"].ToString();
            userInfo.FullName = userInfo.Username.Split('@')[0]; // TODO: This should be changed to use the real Full Name of the users
            userInfo.RoleLookup = new Dictionary<string, int>();

            // Lookup roles for this current user
            SqlGenerator sqlGenLevels = new SqlGenerator(SqlGenerator.SqlTypes.Select, "UserRight", true);
            sqlGenLevels.AddTable("SecurityObject", SqlGenerator.SqlJoins.Inner, "SecurityObject_Key");
            sqlGenLevels.AddField("ObjectTitle", "SecurityObject");
            sqlGenLevels.AddField("SecurityLevel", "UserRight");
            sqlGenLevels.AddWhereParameter("UserRight", "SecurityUser_Key", userInfo.OldUserKey, SqlWhereComparison.SqlComparer.Equal);

            // Loop through all of our role levels and assign them to our AuthUserInformationModel.RoleLookup dictionarys
            using (SqlDataReader r = Adocls.FetchDataReader(sqlGenLevels, "UserDatabase"))
            {
                while (r.Read())
                {
                    userInfo.RoleLookup.Add((string)r["ObjectTitle"], (byte)r["SecurityLevel"]);
                }
            }

            return userInfo;
        }
    }
}
