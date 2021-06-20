using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUngDung.Areas.Admin.Code
{
    public class SessionHelper
    {

        public static void setSession(UserSession session)
        {
            HttpContext.Current.Session["Admin"] = session;
        }

        public static UserSession getSession()
        {
            var session = HttpContext.Current.Session["Admin"];
            if(session == null)
            {
                return null;
            }
            else
            {
                return session as UserSession;
            }
        }
    }
}