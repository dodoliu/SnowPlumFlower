﻿using System;
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
    public class BeetleController:BaseController
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
        public JsonResult GetCCList(string sid="")
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);
            try
            {
                BeetleClassContent bcc = new BeetleClassContent();
                rm.ResultData["CCIList"] = bcc.GetModelList(string.Format("1=1 and BCSid = '{0}'",Server.UrlEncode(sid)));
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
                IList<BeetlePicInfo> bpiList = bp.GetModelList(string.Format("1=1 and BPStatus = 1 and BCCSid = '{0}'",Server.UrlEncode(sid)));
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

        #endregion

    }
}
