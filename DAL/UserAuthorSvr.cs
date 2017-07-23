using System.Linq;
using DAL.MB;
using Model;
using System.Collections.Generic;

namespace DAL
{
    public class UserAuthorSvr
    {
        public virtual bool UserLogin(string LoginName, string PassWord)
        {
            using (var db = new MbContext())
            {
                var user = db.User.FirstOrDefault(x => x.LoginName == LoginName);
                if (user != null)
                {
                    if (PassWord == user.PassWord)
                    {
                        var session = System.Web.HttpContext.Current.Session;
                        session["UserId"] = user;
                        return true;
                    }
                    return false;
                }
                
                else
                {
                    return false;
                }
            }
        }


        public static Dictionary<string, User> App_Users { get; set; }

        static UserAuthorSvr()
        {
            App_Users = new Dictionary<string, User>();
        }
        public static User CurrentUser
        {
            get
            {
                var request = System.Web.HttpContext.Current.Request;
                var userKey = request["userKey"];
                if (string.IsNullOrEmpty(userKey))
                {
                    return System.Web.HttpContext.Current.Session["UserId"] as User; ;
                }
                else
                {
                    if (App_Users != null && App_Users.ContainsKey(userKey))
                    {
                        return App_Users[userKey];
                    }
                }
                return null;
            }
        }

    }
}
