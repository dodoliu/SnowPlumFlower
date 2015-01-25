using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Mvc;
using SPF.OleDB.BLL;
using SPF.OleDB.Model;

namespace SPF.Beetlea.Web.Controllers
{
    public class BeetleController : BaseController
    {
        public BeetleController()
        {

        }

        #region 前台
        //public ActionResult Index()
        //{
        //    return View();
        //}
        /// <summary>
        /// 分类展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassContent()
        {
            return View();
        }
        /// <summary>
        /// 获取分类内容
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public JsonResult GetCCList(string sid = "")
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);
            try
            {
                BeetleClassContent bcc = new BeetleClassContent();
                rm.ResultData["CCIList"] = bcc.GetModelList(string.Format("1=1 and BCSid = '{0}'", Server.UrlEncode(sid)));
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }


            return MyJson(rm);
        }

        /// <summary>
        /// 服务流程
        /// </summary>
        /// <returns></returns>
        public ActionResult BeetleService()
        {
            return View();
        }

        /// <summary>
        /// 二级页面,图片展示
        /// </summary>
        /// <returns></returns>
        public ActionResult BeetlePicShow()
        {
            return View();
        }

        public JsonResult GetPicList(string sid = "")
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage();

            try
            {
                BeetlePic bp = new BeetlePic();
                IList<BeetlePicInfo> bpiList = bp.GetModelList(string.Format("1=1 and BPStatus = 1 and BCCSid = '{0}'", Server.UrlEncode(sid)));
                rm.ResultData["BPIList"] = bpiList;
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }

            return MyJson(rm);

        }
        /// <summary>
        /// 留言板
        /// </summary>
        /// <returns></returns>
        public ActionResult BeetleComment()
        {
            return View();
        }
        /// <summary>
        /// 微信预约
        /// </summary>
        /// <returns></returns>
        public ActionResult BeetleSubscribe()
        {
            return View();
        }

        public JsonResult SubScribeSubmit()
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);
            try
            {
                string strContact = ReplaceString(Request["con"]);
                string strInformation = ReplaceString(Request["inf"]);
                string strReserveDate = ReplaceString(Request["res"]);
                string strHouseType = ReplaceString(Request["typ"]);
                string strHouseAddress = ReplaceString(Request["add"]);
                string strFloorArea = ReplaceString(Request["flo"]);
                string strStyle = ReplaceString(Request["sty"]);
                string strMemo = ReplaceString(Request["mem"]);                 
                //存数据库

                BeetleSubscribe bs = new BeetleSubscribe();
                BeetleSubscribeInfo bsi = new BeetleSubscribeInfo();
                //如果联系人/联系方式相同,则认为是同一个人
                bsi = bs.GetModel(strContact, strInformation);
                if (bsi == null)
                {
                    bsi = new BeetleSubscribeInfo();
                    bsi.SubsContact = strContact;
                    bsi.SubsInformation = strInformation;
                    bsi.SubsReserveDate = Convert.ToDateTime(strReserveDate);
                    bsi.SubsHouseType = strHouseType;
                    bsi.SubsHouseAddress = strHouseAddress;
                    bsi.SubsFloorArea = strFloorArea;
                    bsi.SubsStyle = strStyle;
                    bsi.SubsMemo = strMemo;
                    bsi.SubsSid = Guid.NewGuid().ToString();
                    bsi.SubsStatus = 1;
                    bsi.SubsCreateDate = DateTime.Now;
                    bs.Add(bsi);
                }
                else
                {
                    bsi.SubsReserveDate = Convert.ToDateTime(strReserveDate);
                    bsi.SubsHouseType = strHouseType;
                    bsi.SubsHouseAddress = strHouseAddress;
                    bsi.SubsFloorArea = strFloorArea;
                    bsi.SubsStyle = strStyle;
                    bsi.SubsMemo = strMemo;
                    bsi.SubsSid = Guid.NewGuid().ToString();
                    bsi.SubsStatus = 1;
                    bs.Update(bsi);
                }

                //发送邮件

                StringBuilder sb = new StringBuilder();
               
                sb.Append("微信预约:<br/>");
                sb.AppendFormat("联系人:{0}<br/>", strContact);
                sb.AppendFormat("联系方式:{0}<br/>", strInformation);
                sb.AppendFormat("预定日期:{0}<br/>", strReserveDate);
                sb.AppendFormat("户型:{0}<br/>", strHouseType);
                sb.AppendFormat("小区地址:{0}<br/>", strHouseAddress);
                sb.AppendFormat("建筑面积:{0}<br/>", strFloorArea);
                sb.AppendFormat("期望风格:{0}<br/>", strStyle);
                sb.AppendFormat("指定设计师或备注:{0}<br/>", strMemo);

                new Helper.SendEmailHelper().SendEmailFunc(Helper.TextHelper.GetConfigItem("SmtpServer_SendMailList"), "微信预约_" + strContact, sb.ToString(), "");

                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }

            return MyJson(rm);
        }

        private string ReplaceString(string content)
        {
            return content.Trim().Replace(" ", "").Replace("~", "").Replace("@", "")
                    .Replace("$", "").Replace("%", "").Replace("^", "").Replace("*", "").Replace("#", "");
        }

        #endregion

    }
}
