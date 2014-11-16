using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;

namespace SPF.Controller
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
