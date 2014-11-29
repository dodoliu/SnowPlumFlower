using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM = System.Configuration.ConfigurationManager;

namespace SPF.Helper
{
    /// <summary>
    /// 发送邮件的公共类
    /// </summary>
    public class SendEmailHelper
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public readonly string SMTP_SERVER_NAME = CM.AppSettings["SmtpServerName"];
        /// <summary>
        /// 登录名
        /// </summary>
        public readonly string LOGIN_NAME = CM.AppSettings["SmtpServer_LoginName"];
        /// <summary>
        /// 密码
        /// </summary>
        public readonly string LOGIN_PASSWORD = CM.AppSettings["SmtpServer_LoginPassword"];
        /// <summary>
        /// 发送邮件地址
        /// </summary>
        public readonly string FROM_EMAIL_ADDRESS = CM.AppSettings["SmtpServer_FromMail"];

        private System.Net.Mail.SmtpClient SmtpServer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SendEmailHelper()
        {
            SmtpServer = new System.Net.Mail.SmtpClient(SMTP_SERVER_NAME);
            SmtpServer.Credentials = new System.Net.NetworkCredential(LOGIN_NAME.Trim(), LOGIN_PASSWORD.Trim());
        }

        #region 发邮件
        /// <summary>发邮件</summary>
        /// <param name="emailList">发件人</param>
        /// <param name="topic">主题</param>
        /// <param name="content">正文</param>
        /// <param name="attachPath">附件</param>
        public ReturnMessage SendEmailFunc(string emailList, string topic, string content, string attachPath)
        {
            ReturnMessage rm = new ReturnMessage(true);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(FROM_EMAIL_ADDRESS.Trim());

            string[] mailtolist = emailList.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in mailtolist)
            {
                mail.To.Add(str.Trim());
            }
            mail.Subject = topic;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            //附件
            string cid = "";
            if (attachPath != null && attachPath.Length > 0)
            {
                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(attachPath);
                attachment1.Name = System.IO.Path.GetFileName(attachPath);
                attachment1.NameEncoding = System.Text.Encoding.GetEncoding("GB2312");
                attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                attachment1.ContentDisposition.Inline = true;
                attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                cid = attachment1.ContentId;
                mail.Attachments.Add(attachment1);
            }
            //邮件正文
            mail.Body = content.Trim();
            SmtpServer.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(SmtpServer_SendCompleted);
            try
            {
                //必须同步发送,异步不行....
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                rm.IsSuccess = false;
                rm.Exception = e;
            }

            return rm;
        }

        /// <summary>发邮件</summary>
        /// <param name="emailList">发件人</param>
        /// <param name="topic">主题</param>
        /// <param name="content">正文</param>
        /// <param name="attachPath">附件</param>
        public ReturnMessage SendEmailFunc2(string emailList, string topic, string content, string attachPath)
        {
            return SendEmailFunc3(emailList, topic, content, new[] {attachPath});
        }

        /// <summary>发邮件</summary>
        /// <param name="emailList">发件人</param>
        /// <param name="topic">主题</param>
        /// <param name="content">正文</param>
        /// <param name="attachPath">附件地址集合</param>
        public ReturnMessage SendEmailFunc3(string emailList, string topic, string content, string[] attachPath)
        {
            ReturnMessage rm = new ReturnMessage(true);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(FROM_EMAIL_ADDRESS.Trim());

            string[] mailtolist = emailList.Split(';');
            foreach (string str in mailtolist)
            {
                mail.To.Add(str.Trim());
            }
            mail.Subject = topic;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            //附件
            string cid = "";
            if (attachPath != null && attachPath.Length > 0)
            {
                foreach (string attach in attachPath)
                {
                    if (!string.IsNullOrWhiteSpace(attach))
                    {
                        System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(attach);
                        attachment1.Name = System.IO.Path.GetFileName(attach);
                        attachment1.NameEncoding = System.Text.Encoding.GetEncoding("GB2312");
                        attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        attachment1.ContentDisposition.Inline = true;
                        attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                        cid = attachment1.ContentId;
                        mail.Attachments.Add(attachment1);
                    }
                }
               
            }
            //邮件正文
            mail.Body = content.Trim();
            SmtpServer.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(SmtpServer_SendCompleted);
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                rm.IsSuccess = false;
                rm.Exception = e;
            }

            return rm;
        }

        void SmtpServer_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string token = Convert.ToString(e.UserState);
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
                new ExceptionHelper().LogException(e.Error);
            }
            else if (token == "发送成功!")
            {
                Console.WriteLine("[{0}] 发送成功!", token);
            }
        }
        #endregion

    }
}
