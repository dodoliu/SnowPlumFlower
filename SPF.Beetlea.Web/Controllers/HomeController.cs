using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SPF.OleDB.BLL;

namespace SPF.Beetlea.Web.Controllers
{
    public class HomeController : BaseController
    {

        //GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClassContentForIndex(string sids)
        {
            Helper.ReturnMessage rm = new Helper.ReturnMessage();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("1=1 and (");
                StringBuilder sbs = new StringBuilder();
                sids.Split(new char[] {','}).AsEnumerable().All(p =>
                {
                    sbs.AppendFormat("BCSid = '{0}' or ", p);
                    return true;
                });

                sb.AppendFormat("{0})", sbs.ToString().Substring(0, sbs.Length-3));
                sb.Append(" and BCCStatus = 1");

                BeetleClassContent bcc = new BeetleClassContent();
                rm.ResultData["CCIList"] = bcc.GetModelList(sb.ToString());
                rm.IsSuccess = true;
            }
            catch
            {
                rm.IsSuccess = false;
            }
            return MyJson(rm);
        }


    }
}
