using ApiModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Tool_Web.Base;
using Tool_Web.Common;
using Tool_Web.Models.Login;

namespace Tool_Web.Controllers.Login
{
    public class LoginController : ApiBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisteredVaild(string VaildID,string IsVue="N")
        {
            LoginR resultMode = new LoginR();
            TempData["IsVue"] = IsVue;
            if (VaildID == null)
            {
                if (Session["LoginModel"] != null)
                    resultMode = Session["LoginModel"] as LoginR;
                else
                    resultMode.MSG = "無法導向網頁";
                return View(resultMode);
            }
            else if (VaildID == "Vaild")
            {
                resultMode.MSG = TempData["Msg"].ToString();
                return View("~/Views/Login/RegisteredVaild.cshtml", resultMode);
            }

            LoginM model = new LoginM();
            model.VaildID = VaildID;
            resultMode = CallApi<LoginM, LoginR>("Api/Login/VaildRegistered", model);

            if (resultMode == null)
            {
                resultMode = new LoginR();
                resultMode.IS_Error = "Y";
            }

            return View(resultMode);
        }

        public ContentResult RegisteredVaildStyle()
        {
            string css = "height: {0}px;";
            if (Convert.ToString(TempData["IsVue"]) == "Y")
            {
                css = @"#top { " + string.Format(css, "20") + "} ";
                css += @"#MSG { left: 50% !important;
                                margin-left: -125px !important;
                                width: 250px !important; } ";
            }
            else
            {
                css = @"#top { " + string.Format(css, "100") + "} ";
            }

            return Content(css, "text/css");
        }

        public ActionResult ResendMail(string UID, string Email)
        {
            if (UID == null || Email == null)
                return new HttpNotFoundResult();

            TempData["UID"] = UID;
            ViewBag.Email = Email.Replace("!!!", ".");
            return View();

        }

        public ActionResult SendMail(string Email)
        {
            Models.Login.UserData model = new Models.Login.UserData();
            model.Uid = TempData["UID"].ToString();
            model.Email = Email;
            Models.BaseR result = CallCoreApi<Models.Login.UserData, Models.BaseR>("Api/ResendMail", model);
            TempData["Msg"] = result.Data;
            return RegisteredVaild("Vaild","Y");

        }

        public ActionResult Registered()
        {
            return View();

        }

        public ActionResult SetLogin(LoginM model)
        {
            LoginR resultMode = CallApi<LoginM, LoginR>("Api/Login/SetLogin", model);
            if (resultMode == null)
            {
                resultMode = new LoginR();
                resultMode.IS_Error = "Y";
            }
            if (resultMode.IS_Error == "N")
                Session.Add("LoginModel", resultMode);

            DataContractJsonSerializer json = new DataContractJsonSerializer(resultMode.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, resultMode);
                string ResultJson = Encoding.UTF8.GetString(ms.ToArray());
                return Json(ResultJson);
            }

        }

        public ActionResult ChkUserData(LoginM model)
        {
            LoginR resultMode = CallApi<LoginM, LoginR>("Api/Login/ChkUserData", model);
            if (resultMode == null)
            {
                resultMode = new LoginR();
                resultMode.IS_Error = "Y";
                resultMode.MSG = "使用者帳號無法驗證，請重新輸入";
            }

            DataContractJsonSerializer json = new DataContractJsonSerializer(resultMode.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, resultMode);
                string ResultJson = Encoding.UTF8.GetString(ms.ToArray());
                return Json(ResultJson);
            }
        }

        public void UseEmail()
        {
            Email email = new Email();
            email.UseGmail();
        }
    }
}