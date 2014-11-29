using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SPF.DBUtility;
using SPF.OleDB.BLL;
using SPF.OleDB.Model;

namespace SPF.Beetlea.Web.Controllers
{
    [Authorize]
    public class BeetleManagerController:BaseController
    { 
        #region 主分类
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
        public JsonResult GetClassList(int ipageindex = 0,int ipagesize=10)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                BeetleClass bc = new BeetleClass();
                //分页
                DataHelpler dataHelper = new DataHelpler();
                //总记录条数
                int iCount = dataHelper.FindCount("BeetleClass", "ID");
                //每页记录数
                int iPageSize = ipagesize;
                //总页数
                int iPageCount = iCount % iPageSize == 0 ? iCount / iPageSize : iCount / iPageSize + 1;
                //当前页
                int iPageIndex = ipageindex <= 0 ? 1 : ipageindex;

                DataTable dt = dataHelper.FindDataByPage(iPageSize, iPageIndex, "BeetleClass", "ID");
                IList <BeetleClassInfo> bciList= bc.DataTableToList(dt);

                rm.ResultData["BCIList"] = bciList;
                rm.ResultData["iPageSize"] = iPageSize;
                rm.ResultData["iPageCount"] = iPageCount;
                rm.ResultData["iPageIndex"] = iPageIndex;
                rm.ResultData["iCount"] = iCount;

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

        #region 子分类
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
        public JsonResult GetClassContentList(string bccname = "",string bcsid="", int ipageindex = 0, int ipagesize = 10)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                //获取分类信息
                BeetleClass bc = new BeetleClass();
                StringBuilder bcsb = new StringBuilder();
                bcsb.Append("1=1");
                IList<BeetleClassInfo> bciList = bc.GetModelList(bcsb.ToString());

                //分类内容
                StringBuilder sb = new StringBuilder();
                sb.Append("1=1");
                if (!string.IsNullOrWhiteSpace(bccname))
                {
                    sb.AppendFormat(" and BCCName like '%{0}%'", bccname);
                }
                if (!string.IsNullOrWhiteSpace(bcsid))
                {
                    sb.AppendFormat(" and BCSid = '{0}'", bcsid);
                }
                BeetleClassContent bcc = new BeetleClassContent();

                //分页
                DataHelpler dataHelper = new DataHelpler();
                //总记录条数
                int iCount = dataHelper.FindCount("BeetleClassContent", "ID",sb.ToString());
                //每页记录数
                int iPageSize = ipagesize;
                //总页数
                int iPageCount = iCount % iPageSize == 0 ? iCount / iPageSize : iCount / iPageSize + 1;
                //当前页
                int iPageIndex = ipageindex <= 0 ? 1 : ipageindex;

                DataTable dt = dataHelper.FindDataByPage(iPageSize, iPageIndex, "BeetleClassContent", "ID", sb.ToString());


                //IList<BeetleClassContentInfo> bcciList = bcc.GetModelList(sb.ToString());

                IList<BeetleClassContentInfo> bcciList = bcc.DataTableToList(dt);

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
                rm.ResultData["iPageSize"] = iPageSize;
                rm.ResultData["iPageCount"] = iPageCount;
                rm.ResultData["iPageIndex"] = iPageIndex;
                rm.ResultData["iCount"] = iCount;
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

        #region 图片展示

        public ActionResult ManagerPicShow()
        {
            return View();

        }

        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="bccname"></param>
        /// <param name="bcsid"></param>
        /// <param name="ipageindex"></param>
        /// <param name="ipagesize"></param>
        /// <returns></returns>
        public JsonResult GetPicList(string pname = "", string bccsid = "", int ipageindex = 0, int ipagesize = 10)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                //获取分类信息
                BeetleClassContent bcc = new BeetleClassContent();
                StringBuilder bccsb = new StringBuilder();
                bccsb.Append("1=1");
                IList<BeetleClassContentInfo> bciList = bcc.GetModelList(bccsb.ToString());

                //分类内容
                StringBuilder sb = new StringBuilder();
                sb.Append("1=1");
                if (!string.IsNullOrWhiteSpace(pname))
                {
                    sb.AppendFormat(" and BPName like '%{0}%'", pname);
                }
                if (!string.IsNullOrWhiteSpace(bccsid))
                {
                    sb.AppendFormat(" and BCCSid = '{0}'", bccsid);
                }
                BeetlePic bp = new BeetlePic();

                //分页
                DataHelpler dataHelper = new DataHelpler();
                //总记录条数
                int iCount = dataHelper.FindCount("BeetlePic", "ID", sb.ToString());
                //每页记录数
                int iPageSize = ipagesize;
                //总页数
                int iPageCount = iCount % iPageSize == 0 ? iCount / iPageSize : iCount / iPageSize + 1;
                //当前页
                int iPageIndex = ipageindex <= 0 ? 1 : ipageindex;

                DataTable dt = dataHelper.FindDataByPage(iPageSize, iPageIndex, "BeetlePic", "ID", sb.ToString());


                IList<BeetlePicInfo> bpiList = bp.DataTableToList(dt);

                IList<ClassContentAndPicInfo> ccapiList = new List<ClassContentAndPicInfo>();
                bpiList.AsEnumerable().All(p =>
                {
                    string strBCCName = bciList.Where(s => s.BCCSid == p.BCCSid).Select(s => s.BCCName).FirstOrDefault();
                    ccapiList.Add(new ClassContentAndPicInfo
                    {
                        BCCSid = p.BCCSid,
                        BCCName = string.IsNullOrWhiteSpace(strBCCName) ? "未知" : strBCCName,
                        BCCUpdateTime = p.BCCUpdateTime,
                        BPDemo = p.BPDemo,
                        BPName = p.BPName,
                        BPStatus = p.BPStatus,
                        BPUrl = p.BPUrl,
                        ID = p.ID,
                        BCCCreateTime = p.BCCCreateTime
                    });
                    return true;
                });

                rm.ResultData["BPIList"] = ccapiList;
                rm.ResultData["iPageSize"] = iPageSize;
                rm.ResultData["iPageCount"] = iPageCount;
                rm.ResultData["iPageIndex"] = iPageIndex;
                rm.ResultData["iCount"] = iCount;
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }

            return Json(rm);
        }
        /// <summary>
        /// 获取单个图片对象
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPicInfo(int id = 0)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                if (id > 0)
                {
                    BeetlePic bc = new BeetlePic();
                    BeetlePicInfo bpi = bc.GetModel(id);
                    rm.ResultData["BPInfo"] = bpi;
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
        /// 添加图片对象
        /// </summary>
        /// <returns></returns>
        public JsonResult AddPicInfo(BeetlePicInfo bcci)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                bool _bool = false;
                if (bcci != null)
                {
                    BeetlePic bcc = new BeetlePic();
                    DateTime nowTime = DateTime.Now;
                    if (bcci.ID <= 0)
                    { 
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
        /// 获取指定的子分类
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClassContentForPic()
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);
            try
            {
                BeetleClassContent bcc = new BeetleClassContent();
                IList<BeetleClassContentInfo> bcciList = bcc.GetModelList(string.Format("1=1 and BCSid = '4ee947de-60c6-41b9-94b0-4007241fd5c3' or BCSid = '576b4621-9455-4d89-97bd-d30aab013785' or BCSid = '3f3bb325-c531-4d3c-bb1e-8e1badff3417' "));
                rm.ResultData["BCCIList"] = bcciList;
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }
            return MyJson(rm);
        }


        #endregion

        #region 后台登陆
        /// <summary>
        /// 显示登陆界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Login(string uname,string upwd)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage();





            return MyJson(rm);
        }

        #endregion
    }


    /// <summary>
    /// 主分类和子分类对象
    /// </summary>
    public class ClassAndClassContentInfo : BeetleClassContentInfo
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string BCName { get; set; }
    }
    /// <summary>
    /// 子分类和图片对象
    /// </summary>
    public class ClassContentAndPicInfo : BeetlePicInfo
    {
        /// <summary>
        /// 子分类名称
        /// </summary>
        public string BCCName { get; set; }
    }
}
