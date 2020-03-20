using CoreApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Tool_Web.Base;
using Tool_Web.Models;
using Tool_Web.SQL;

namespace Tool_Web.Controllers
{
    public class HomeController : ApiBase
    {
        public ActionResult Index()
        {
            ViewBag.VueWeb = ConfigurationManager.AppSettings.Get("VueWeb");
            ViewBag.CoreApiUrl = ConfigurationManager.AppSettings.Get("CoreApiServiceUrl");
            ViewBag.cssWH = "000000";

            return View();
        }

        public ActionResult IndexRwd(string id)
        {
            ViewBag.RwdSet = "Y";
            ViewBag.cssWH = id;
            ViewBag.CoreApiUrl = ConfigurationManager.AppSettings.Get("CoreApiServiceUrl");
            return View("~/Views/Home/Index.cshtml");
        }

        public ContentResult SetStyle(string UID="")
        {
            HtmlCss data = new HtmlCss();
            data.PageCode = "Index";
            data.Uid = "System";
            if (UID != "")
                data.Uid = UID;

            Models.BaseR result= CallCoreApi<HtmlCss, Models.BaseR>("Api/GetHtmlCss", data);
            return Content(result.Data as string, "text/css"); 
        }


        public ActionResult NEW_CONNECT()
        {
            //限定同網站的Ajax專用
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/SQL/NewConnect.cshtml");
            }
            else
            {
                return Content("Fail");
            }

        }

       // [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Registered()
        {
            return PartialView("~/Views/Login/Registered.cshtml");
        }

        // [OutputCache(NoStore = true, Duration = 0)]
        public string SetSession()
        {
            return "Y";
        }
    }
}