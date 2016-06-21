using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DPNadminPanel.Models;
using System.Web.Security;
using System.IO;
namespace DPNadminPanel.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                loginValidation obj = new loginValidation(); ;
                return Login(obj);
            }

        }

        public ActionResult Login(loginValidation obj)
        {
            return View(obj);
        }

        [HttpPost]
        public ActionResult Login(loginValidation model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string user = model.UserName;
                string password = "test";
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                }

                if (model.Password == password && model.UserName == "test")
                {

                   // Request.Cookies["userid"].Value = user;
                    
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Request.Cookies["userid"].Value = null;
            return RedirectToAction("Login", "Home");
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
        public JsonResult SaveLayout(string dashboardLayout)
        {
            if (dashboardLayout != null && dashboardLayout.Count() > 0)
            {
                using (StreamWriter sw = new StreamWriter(Server.MapPath("~/Content/DashboardLayout.json")))
                {
                    sw.Write(dashboardLayout);
                }
            }
            return Json(null);
        }

    }
}