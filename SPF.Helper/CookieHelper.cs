using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace SPF.Helper
{
    /// <summary>
    /// CookieHelper
    /// </summary>
    public static class CookieHelper
    {
        /// <summary>
        /// 写入加密cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        public static void WriteEncryptCookie(string name, string value, int minute = 43200)
        {
            if (HttpContext.Current != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1011, name, DateTime.Now, DateTime.Now.AddMinutes(minute), true, value, FormsAuthentication.FormsCookiePath);
                string encryptTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(name,encryptTicket);

                if (minute > 0)
                {
                    cookie.Expires = DateTime.Now.AddMinutes(minute);
                }
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        /// <summary>
        /// 写入加密cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        public static void WriteEncryptCookie(string name, string key, string value, int minute = 43200)
        {
            if (HttpContext.Current != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1011, name, DateTime.Now, DateTime.Now.AddMinutes(minute), true, value, FormsAuthentication.FormsCookiePath);
                string encryptTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                if (cookie == null)
                {
                   cookie = new HttpCookie(name);
                }
                if (minute > 0)
                {
                    cookie.Expires = DateTime.Now.AddMinutes(minute);
                }
                cookie[key] = encryptTicket;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        /// <summary>
        /// 读取加密cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ReadEncryptCookie(string name)
        {
            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                if (cookie != null)
                {
                    try
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                        return ticket.UserData;
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 读取加密cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadEncryptCookie(string name,string key)
        {
            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                if (cookie != null && cookie[key] != null)
                {
                    try
                    {
                        string value = cookie[key];
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(value);
                        return ticket == null ? string.Empty : ticket.UserData;
                    }
                    catch
                    {
                    }
                }
            }
            return string.Empty;
        }



        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public static void WriteCookie(string name,string value)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if(cookie == null)
            {
                cookie = new HttpCookie(name);
            }
            cookie.Value = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void WriteCookie(string name,string key,string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if(cookie == null)
            {
                cookie = new HttpCookie(name);
            }
            cookie[key] = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string name,string value,int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if(cookie == null)
            {
                cookie = new HttpCookie(name);
            }
            cookie.Value = HttpUtility.UrlEncode(value);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>cookie值</returns>
        public static string ReadCookie(string name)
        {
            if(HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[name] != null)
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[name].Value.ToString());

            return string.Empty;
        }


        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="key">名称</param>
        /// <returns>cookie值</returns>
        public static string ReadCookie(string name,string key)
        {
            if(HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[name] != null && HttpContext.Current.Request.Cookies[name][key] != null)
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[name][key].ToString());

            return string.Empty;
        }
    }
}
