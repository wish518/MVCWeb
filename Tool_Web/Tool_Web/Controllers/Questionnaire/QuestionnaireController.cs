using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tool_Web.Controllers.Questionnaire
{
    public class QuestionnaireController : Controller
    {
        // GET: Questionnaire
        public ActionResult Index()
        {
            ViewBag.Title = "建立問卷";
            return View("~/Views/Home/Error.cshtml");
        }
    }
}