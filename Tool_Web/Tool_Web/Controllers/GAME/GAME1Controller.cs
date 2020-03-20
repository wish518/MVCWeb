using ApiModel.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tool_Web.Base;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Tool_Web.Controllers.GAME
{
    public class GAME1Controller : ApiBase
    {
        /*  // GET: GAME1
          public ActionResult Index()
          {
              /*resultMode = (Game1R) ViewData.Model;
              return View(resultMode);
          }
          public ActionResult GAME1View()
          {
              return View("~/Views/GAME/GAME1.cshtml", resultMode);
          }*/


        public ActionResult GetCard()
        {
            Game1R resultMode = Session["MODEL"] as Game1R;
            GetCardM aa = new GetCardM();
            aa.UserPlay = resultMode.UserPlay;
            aa.Round = resultMode.ROUND;
            var result = CallApi<GetCardM, List<GetCardR>>("Api/GAME/GetCard", aa);
            resultMode.GetCardRList = result;
            DataContractJsonSerializer json = new DataContractJsonSerializer(resultMode.GetType());
            ViewData.Model = resultMode;
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, resultMode);
                string ResultJson = Encoding.UTF8.GetString(ms.ToArray());
                return Json(ResultJson);
            }
            // PartialView("~/Views/GAME/GAME1.cshtml", resultMode);
            //return Json(json);
            //return View("~/Views/Home/Index.cshtml", resultMode);
        }
        public ActionResult Pick(int Index)
        {
            Game1R resultMode = Session["MODEL"] as Game1R;
            resultMode.IS_Error = "N";
            var Data = resultMode.GetCardRList.Where(o => o.CardIndex == Index);
            DataContractJsonSerializer json;

            if (Data.Count() > 0 && resultMode.GetCardRList.Where(o => o.IS_PICK == "Y").Count() != 3)
            {
                Data.First().IS_PICK = Data.First().IS_PICK == "Y" ? "N" : "Y";
                Data.First().IS_Error = "N";
            }
            else
            {
                if (resultMode.GetCardRList.Where(o => o.IS_PICK == "Y").Count() != 3)
                    resultMode.IS_Error = "0";//已選三張錯誤代碼
                else
                    resultMode.IS_Error = "Y";
                json = new DataContractJsonSerializer(resultMode.GetType());

                using (MemoryStream ms = new MemoryStream())
                {
                    json.WriteObject(ms, resultMode);
                    string ResultJson = Encoding.UTF8.GetString(ms.ToArray());
                    return Json(ResultJson);
                }
            }
            if (resultMode.GetCardRList.Where(o => o.IS_PICK == "Y").Count() > 3)
                Data.First().IS_Error = "Y";
            json = new DataContractJsonSerializer(Data.First().GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, Data.First());
                string ResultJson = Encoding.UTF8.GetString(ms.ToArray());
                return Json(ResultJson);
            }
        }

        public ActionResult SetCard()
        {
            Game1R resultMode = Session["MODEL"] as Game1R;
            resultMode.IS_Error = "N";

            if (resultMode.GetCardRList.Where(o => o.IS_PICK == "Y").Count() == 3 &&
                resultMode.GetCardRList.Where(o => o.IS_PICK != "Y").Count() == 2)
            {
                foreach (GetCardR CardR in resultMode.GetCardRList.Where(o => o.IS_PICK == "Y"))
                {
                    if (CardR.IS_PICK == "Y")
                    {
                        UserCard userCardR = new UserCard();
                        userCardR.UserPlay = resultMode.UserPlay;
                        userCardR.Round = resultMode.ROUND;
                        userCardR.Card = CardR.Card;
                        resultMode.GetUserCardList.Add(userCardR);
                    }
                    else
                    {
                        ComCard ComCardR = new ComCard();
                        ComCardR.Round = resultMode.ROUND;
                        ComCardR.Card = CardR.Card;
                        resultMode.GetComCardList.Add(ComCardR);
                    }
                }

                var result = CallApi<Game1R, Game1R>("Api/GAME/SetCard", resultMode);
                resultMode = result;
                if (resultMode.IS_Error == "N")
                    resultMode.Status = "請配置卡片";

            }
            else
            {
                resultMode.IS_Error = "Y";
            }

            DataContractJsonSerializer json = new DataContractJsonSerializer(resultMode.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, resultMode);
                string ResultJson = Encoding.UTF8.GetString(ms.ToArray());
                return Json(ResultJson);
            }
        }
    }
}