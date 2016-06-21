using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPNadminPanel.Controllers
{
    public class LayoutsController : Controller
    {
        //
        // GET: /Layouts/
        public ActionResult Layout1()
        {
            return PartialView();
        }
        public ActionResult Layout2()
        {
            return PartialView();
        }
        public ActionResult Layout3()
        {
            return PartialView();
        }
	}
}