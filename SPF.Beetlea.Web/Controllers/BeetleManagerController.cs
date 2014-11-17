using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SPF.OleDB.BLL;
using SPF.OleDB.Model;

namespace SPF.Beetlea.Web.Controllers
{
    public class BeetleManagerController:BaseController
    {



        #region 后台

        #region 分类
        /// <summary>
        /// 请求
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagerClass()
        {
            return View();
        }
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClassList()
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                BeetleClass bc = new BeetleClass();
                //分页
                Helper.DataHelpler dataHelper = new Helper.DataHelpler();
                //总记录条数
                int iCount = dataHelper.FindCount("BeetleClass", "ID");
                //每页记录数
                int iPageSize = 3;
                //总页数
                int iPageCount = iCount/2 + 1;
                int iPageIndex = 2;

                DataTable dt = dataHelper.FindDataByPage(iPageSize, iPageIndex, "BeetleClass", "ID");



                IList<BeetleClassInfo> bciList = new List<BeetleClassInfo>();
                bciList = bc.GetModelList(string.Format("1=1"));
                rm.ResultData["BCIList"] = bciList;
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }

            return Json(rm);
        }

        #region 编辑分类

        /// <summary>
        /// 获取单个分类对象
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClassInfo(int id = 0)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                if (id > 0)
                {
                    BeetleClass bc = new BeetleClass();
                    BeetleClassInfo bci = bc.GetModel(id);
                    rm.ResultData["BCInfo"] = bci;
                    rm.IsSuccess = true;
                }
                else
                {
                    rm.IsSuccess = false;
                }
            }
            catch
            {
                rm.IsSuccess = false;
            }
            return Json(rm);
        }

        /// <summary>
        /// 添加分类对象
        /// </summary>
        /// <returns></returns>
        public JsonResult AddClassInfo(BeetleClassInfo bci)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                bool _bool = false;
                if (bci != null)
                {
                    BeetleClass bc = new BeetleClass();
                    DateTime nowTime = DateTime.Now;
                    if (bci.ID <= 0)
                    {
                        bci.BCSid = Guid.NewGuid().ToString();
                        bci.BCCreateTime = nowTime;
                        bci.BCUpdateTime = nowTime;

                        _bool = bc.Add(bci);
                    }
                    else
                    {
                        bci.BCUpdateTime = nowTime;
                        _bool = bc.Update(bci);
                    }
                }

                if (_bool)
                {
                    rm.Text = "保存成功!";
                    rm.IsSuccess = true;
                }
                else
                {
                    rm.Text = "保存失败!";
                    rm.IsSuccess = false;
                }
            }
            catch
            {
                rm.Text = "保存失败!";
                rm.IsSuccess = false;
            }

            return Json(rm);
        }

        #endregion

        #endregion


        #region 分类内容
        /// <summary>
        /// 请求
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagerClassContent()
        {
            return View();
        }

        /// <summary>
        /// 获取分类内容列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClassContentList(string bccname="")
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                //获取分类信息
                BeetleClass bc = new BeetleClass();
                IList<BeetleClassInfo> bciList = bc.GetModelList(string.Format("1=1"));

                //分类内容
                StringBuilder sb = new StringBuilder();
                sb.Append("1=1");
                if (!string.IsNullOrWhiteSpace(bccname))
                {
                    sb.AppendFormat(" and BCCName like '%{0}%'", bccname);
                }
                BeetleClassContent bcc = new BeetleClassContent();
                IList<BeetleClassContentInfo> bcciList = bcc.GetModelList(sb.ToString());
                IList<ClassAndClassContentInfo> caciList = new List<ClassAndClassContentInfo>();
                bcciList.AsEnumerable().All(p =>
                {
                    string strBCName = bciList.Where(s => s.BCSid == p.BCSid).Select(s=>s.BCName).FirstOrDefault();
                    caciList.Add(new ClassAndClassContentInfo
                    {
                        BCCCreateTime = p.BCCCreateTime,
                        BCCDesc = p.BCCDesc,
                        BCCName = p.BCCName,
                        BCCPic = p.BCCPic,
                        BCCSid = p.BCCSid,
                        BCCStatus = p.BCCStatus,
                        BCCType = p.BCCType,
                        BCCUpdateTime = p.BCCUpdateTime,
                        BCCUrl = p.BCCUrl,
                        BCName = string.IsNullOrWhiteSpace(strBCName) ? "未知" : strBCName,
                        BCSid = p.BCSid,
                        ID = p.ID
                    });
                    return true;
                });

                rm.ResultData["BCCIList"] = caciList;
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }

            return Json(rm);
        }

        #region 编辑分类内容

        /// <summary>
        /// 添加分类内容对象
        /// </summary>
        /// <returns></returns>
        public JsonResult AddClassContentInfo(BeetleClassContentInfo bcci)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                bool _bool = false;
                if (bcci != null)
                {
                    BeetleClassContent bcc = new BeetleClassContent();
                    DateTime nowTime = DateTime.Now;
                    if (bcci.ID <= 0)
                    {
                        bcci.BCCSid = Guid.NewGuid().ToString();
                        bcci.BCCCreateTime = nowTime;
                        bcci.BCCUpdateTime = nowTime;

                        _bool = bcc.Add(bcci);
                    }
                    else
                    {
                        bcci.BCCUpdateTime = nowTime;
                        _bool = bcc.Update(bcci);
                    }
                }

                if (_bool)
                {
                    rm.Text = "保存成功!";
                    rm.IsSuccess = true;
                }
                else
                {
                    rm.Text = "保存失败!";
                    rm.IsSuccess = false;
                }
            }
            catch
            {
                rm.Text = "保存失败!";
                rm.IsSuccess = false;
            }

            return Json(rm);
        }


        /// <summary>
        /// 获取单个分类内容对象
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClassContentInfo(int id = 0)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                if (id > 0)
                {
                    BeetleClassContent bc = new BeetleClassContent();
                    BeetleClassContentInfo bcci = bc.GetModel(id);
                    rm.ResultData["BCCInfo"] = bcci;
                    rm.IsSuccess = true;
                }
                else
                {
                    rm.IsSuccess = false;
                }
            }
            catch
            {
                rm.IsSuccess = false;
            }
            return Json(rm);
        }

        #endregion

        #endregion
        #endregion
    }


    /// <summary>
    /// 分类和分类内容对象
    /// </summary>
    public class ClassAndClassContentInfo : BeetleClassContentInfo
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string BCName { get; set; }
    }
}
