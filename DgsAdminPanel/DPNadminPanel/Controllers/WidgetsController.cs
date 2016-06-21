using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace DPNadminPanel.Controllers
{
    public class WidgetsController : Controller
    {
        //
        // GET: /Widgets/
        public ActionResult Widget1()
        {
            return PartialView();
        }
        public ActionResult Widget2()
        {
            return PartialView();
        }
        public ActionResult Widget3()
        {
            return PartialView();
        }
        public ActionResult Widget4()
        {
            return PartialView();
        }
        public ActionResult Widget5()
        {
            return PartialView();
        }
        public ActionResult Widget6()
        {
            return PartialView();
        }

        public string GetLayout()
        {
            string layoutJson = string.Empty;
            using (StreamReader sr = new StreamReader(Server.MapPath("~/Content/DashboardLayout.json")))
            {

                layoutJson = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(layoutJson))
                {
                    return layoutJson;
                }

                return null;
            }


        }

        [HttpPost]
        public JsonResult SaveLayout(string jsonObj)
        {
            if (jsonObj != null && jsonObj.Count() > 0)
            {
                using (StreamWriter sw = new StreamWriter(Server.MapPath("~/Content/DashboardLayout.json")))
                {
                    sw.Write(jsonObj);
                }
            }
            return Json(null);
        }
    }
}