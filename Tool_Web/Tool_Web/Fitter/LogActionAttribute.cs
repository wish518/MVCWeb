using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Tool_Web.Fitter
{
    public class LogActionAttribute: ActionFilterAttribute
    {
        string action, controller, Request;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            action =filterContext.RouteData.Values["action"].ToString();
            controller =filterContext.RouteData.Values["controller"].ToString();
            Request = Convert.ToString(filterContext.HttpContext.Request["ID"] ?? "");

            if (action != "SetSession" && filterContext.HttpContext.Session["ID"] != null)
            {
                WriteLog(1,"進入Action");
                return;
            }
            if (filterContext.HttpContext.Session["EnterAction"] == null)
            {
                filterContext.HttpContext.Session["EnterAction"] = action;
                filterContext.HttpContext.Session["EnterController"] = controller;
                filterContext.HttpContext.Session["Request"] = Request;
            }
            /* else
             {
                 filterContext.HttpContext.Items["LoginOut"] = "Y";
                 WriteLog(filterContext.HttpContext.Session["ID"].ToString(), "自動登出");
             }*/

        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (action == "SetSession" && (Convert.ToString(filterContext.HttpContext.Session["ID"]) ?? "") == "")
            {
                if (Convert.ToString(filterContext.HttpContext.Request["ID"] ?? "") == "")
                {
                    string IP = filterContext.HttpContext.Request["HTTP_X_FORWARDED_FOR"] ?? filterContext.HttpContext.Request["REMOTE_ADDR"];
                    filterContext.HttpContext.Session["ID"] = IP;
                }
                else
                {
                    filterContext.HttpContext.Session["ID"] = filterContext.HttpContext.Request["ID"];
                }
                Request = Convert.ToString(filterContext.HttpContext.Request["Request"]) ?? "";
                WriteLog(1, "進入網站", filterContext.HttpContext.Session["EnterAction"].ToString(), filterContext.HttpContext.Session["EnterController"].ToString());
                filterContext.HttpContext.Session.Remove("EnterAction");
                filterContext.HttpContext.Session.Remove("EnterController");
                filterContext.HttpContext.Session.Remove("Request");
                return;
            };
            if (action != "SetSession" && filterContext.HttpContext.Session["ID"] != null)
                WriteLog(0,"完成Action");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            /*if (Convert.ToString(filterContext.HttpContext.Request["ID"] ?? "") != "")
            {
                WriteLog(0, "產生Result");
            }*/
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (action != "SetSession" && filterContext.HttpContext.Session["ID"] != null)
                WriteLog(0, "完成Result");
        }

        private void WriteLog(int StatusCode,string Status, string Action= "", string Controller= "") {
            if (Action == "")
            {
                Action = action;
                Controller = controller;
            }

            string ID = HttpContext.Current.Session["ID"].ToString();
            string LogPath = ConfigurationManager.AppSettings["UserProcessLogPath"].ToString();
            using (StreamWriter sw = new StreamWriter(string.Format(LogPath, ID.Replace(".","-").Replace(":", "-"), DateTime.Now.ToString("yyyy-MM-dd")), true, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                if (StatusCode == 1)
                {
                    sb.Append("ID=" + ID + " \r\n");
                    sb.Append("ControllerName=" + Controller + ";  ActionName =" + Action + " \r\n");
                    sb.Append("PARM=" + Request + "\r\n");
                }
                sb.Append("DATE=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \r\n");
                sb.Append("Status=" + Status + "\r\n");
                sb.Append("－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－" + "　\r\n");
                sw.Write(sb.ToString());
            }
        }
    }
}