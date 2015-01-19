using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using SPF.Auth;
using SPF.DBUtility;
using SPF.OleDB.BLL;
using SPF.OleDB.Model;
using SPF.Helper;

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

        #region 角色管理
        public ActionResult ManagerRole()
        {
            return View();
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRoleList(int ipageindex = 0, int ipagesize = 10)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                BeetleRole br = new BeetleRole();
                //分页
                DataHelpler dataHelper = new DataHelpler();
                //总记录条数
                int iCount = dataHelper.FindCount("BeetleRole", "ID");
                //每页记录数
                int iPageSize = ipagesize;
                //总页数
                int iPageCount = iCount % iPageSize == 0 ? iCount / iPageSize : iCount / iPageSize + 1;
                //当前页
                int iPageIndex = ipageindex <= 0 ? 1 : ipageindex;

                DataTable dt = dataHelper.FindDataByPage(iPageSize, iPageIndex, "BeetleRole", "ID");
                IList<BeetleRoleInfo> briList = br.DataTableToList(dt);

                rm.ResultData["BRIList"] = briList;
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

        #region 编辑角色

        /// <summary>
        /// 获取单个角色对象
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRoleInfo(int id = 0)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                if (id > 0)
                {
                    BeetleRole bc = new BeetleRole();
                    BeetleRoleInfo bci = bc.GetModel(id);
                    rm.ResultData["BRInfo"] = bci;
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
        /// 添加角色对象
        /// </summary>
        /// <returns></returns>
        public JsonResult AddRoleInfo(BeetleRoleInfo bri)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                bool _bool = false;
                if (bri != null)
                {
                    BeetleRole br = new BeetleRole();
                    DateTime nowTime = DateTime.Now;
                    if (bri.ID <= 0)
                    {
                        bri.BRSid = Guid.NewGuid().ToString();
                        bri.BRCreateDate = nowTime;
                        bri.BRUpdateDate = nowTime;

                        _bool = br.Add(bri);
                    }
                    else
                    {
                        bri.BRUpdateDate = nowTime;
                        _bool = br.Update(bri);
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

        #region 用户管理
        public ActionResult ManagerUser()
        {
            return View();
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserList(string buname = "", string brsid = "", int ipageindex = 0, int ipagesize = 10)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                //获取分类信息
                BeetleRole br = new BeetleRole();
                StringBuilder brsb = new StringBuilder();
                brsb.Append("1=1");
                IList<BeetleRoleInfo> briList = br.GetModelList(brsb.ToString());

                //分类内容
                StringBuilder sb = new StringBuilder();
                sb.Append("1=1");
                if (!string.IsNullOrWhiteSpace(buname))
                {
                    sb.AppendFormat(" and BUName like '%{0}%'", buname);
                }
                if (!string.IsNullOrWhiteSpace(brsid))
                {
                    sb.AppendFormat(" and BRSid = '{0}'", brsid);
                }
                BeetleUser bu = new BeetleUser();

                //分页
                DataHelpler dataHelper = new DataHelpler();
                //总记录条数
                int iCount = dataHelper.FindCount("BeetleUser", "ID", sb.ToString());
                //每页记录数
                int iPageSize = ipagesize;
                //总页数
                int iPageCount = iCount % iPageSize == 0 ? iCount / iPageSize : iCount / iPageSize + 1;
                //当前页
                int iPageIndex = ipageindex <= 0 ? 1 : ipageindex;

                DataTable dt = dataHelper.FindDataByPage(iPageSize, iPageIndex, "BeetleUser", "ID", sb.ToString());

                IList<BeetleUserInfo> buiList = bu.DataTableToList(dt);

                IList<RoleAndUserInfo> caciList = new List<RoleAndUserInfo>();
                buiList.AsEnumerable().All(p =>
                {
                    string strBRName = briList.Where(s => s.BRSid == p.BRSid).Select(s => s.BRName).FirstOrDefault();
                    caciList.Add(new RoleAndUserInfo
                    {
                        BUCreateDate = p.BUCreateDate,
                        BUDesc = p.BUDesc,
                        BUName = p.BUName,
                        BUDisplayName = p.BUDisplayName,
                        BUSid = p.BUSid,
                        BUStatus = p.BUStatus,
                        BUUpdateDate = p.BUUpdateDate,
                        BRName = string.IsNullOrWhiteSpace(strBRName) ? "未知" : strBRName,
                        BRSid = p.BRSid,
                        ID = p.ID
                    });
                    return true;
                });

                rm.ResultData["BUIList"] = caciList;
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

        #region 编辑用户

        /// <summary>
        /// 添加用户对象
        /// </summary>
        /// <returns></returns>
        public JsonResult AddUserInfo(BeetleUserInfo bui)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                bool _bool = false;
                if (bui != null)
                {
                    BeetleUser bu = new BeetleUser();
                    DateTime nowTime = DateTime.Now;
                    if (bui.ID <= 0)
                    {
                       
                        bui.BUSid = Guid.NewGuid().ToString();
                        bui.BUCreateDate = nowTime;
                        bui.BUUpdateDate = nowTime;
                 
                        _bool = bu.Add(bui);
                    }
                    else
                    {
                        bui.BUUpdateDate = nowTime;
                        _bool = bu.Update(bui);
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
        public JsonResult GetUserInfo(int id = 0)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                if (id > 0)
                {
                    BeetleUser bu = new BeetleUser();
                    BeetleUserInfo bui = bu.GetModel(id);
                    rm.ResultData["BUInfo"] = bui;
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

        #region 后台登陆

        /// <summary>
        /// 显示登陆界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            string strUName = CookieHelper.ReadEncryptCookie("userlogoninfo", "uname");
            string strUPwd = CookieHelper.ReadEncryptCookie("userlogoninfo", "upwd");
          
            BeetleUser bu = new BeetleUser();
            BeetleUserInfo bui = bu.GetModelByUserName(strUName, strUPwd);
            if (bui!=null)
            {
                Authorize.SignIn(strUName);
                return Redirect(FormsAuthentication.GetRedirectUrl(strUName, false));
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Loginend(string uname,string upwd)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage(false);

            try
            {
                BeetleUser bu = new BeetleUser();
                BeetleUserInfo bui = bu.GetModelByUserName(uname.Trim().Replace(" ", "").Replace("~", "").Replace("@", "")
                    .Replace("$", "").Replace("%", "").Replace("^", "").Replace("*", "").Replace("#", ""), 
                    upwd.Trim().Replace(" ", ""));

                if (bui != null)
                {
                    Authorize.SignIn(uname);
                    CookieHelper.WriteEncryptCookie("userlogoninfo","uname", uname);
                    CookieHelper.WriteEncryptCookie("userlogoninfo","upwd", upwd);
                    rm.IsSuccess = true;
                    rm.ReturnUrl = FormsAuthentication.GetRedirectUrl(uname, false);
                }
                else
                {
                    rm.Text = "用户名或密码错误!";
                    rm.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }

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

    /// <summary>
    /// 角色用户对象
    /// </summary>
    public class RoleAndUserInfo : BeetleUserInfo
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string BRName { get; set; }
    }
}
