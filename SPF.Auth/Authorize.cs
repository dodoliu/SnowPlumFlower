using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace SPF.Auth
{
    /// <summary>
    /// 用户认证类
    /// </summary>
    public class Authorize
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="accountName">用户名</param>
        public static void SignIn(string accountName)
        {
            FormsAuthentication.SetAuthCookie(accountName, false);
        }

        //public static void SignOut()
        //{
        //    FormsAuthentication.SignOut();
        //    HttpContent.Current
        //}

    }
}
