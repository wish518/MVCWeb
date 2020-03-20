using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tool_Web.Models;

namespace Tool_Web.Base
{
    public class ApiBase : Controller
    {
        public OutModel CallApi<Model, OutModel>(string ApiAdrss, Model model)
        {
            string ApiServiceUrl = ConfigurationManager.AppSettings.Get("ApiServiceUrl");
            var result = (OutModel)Activator.CreateInstance(typeof(OutModel));
            var HClient = new System.Net.Http.HttpClient();
            HClient.BaseAddress = new Uri(ApiServiceUrl);
            HClient.DefaultRequestHeaders.Accept.Clear();
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Task<HttpResponseMessage> HttpResponse = HClient.PostAsJsonAsync(ApiAdrss, model);
            if (HttpResponse.Result.IsSuccessStatusCode)
            {
                var ReModel = HttpResponse.Result.Content.ReadAsAsync<OutModel>();
                return ReModel.Result;
            }
            else
            {
                //result.IsSuccessful = false;
                //result.Message = string.Format("{0}：{1}", ApiAdrss, HttpResponse.Result.RequestMessage);
                return result;
            }
        }

        public OutModel CallCoreApi<Model, OutModel>(string ApiAdrss, Model model)
        {
            string ApiServiceUrl = ConfigurationManager.AppSettings.Get("CoreApiServiceUrl");
            var HClient = new System.Net.Http.HttpClient();
            HClient.BaseAddress = new Uri(ApiServiceUrl);
            HClient.DefaultRequestHeaders.Accept.Clear();
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Task<HttpResponseMessage> HttpResponse = HClient.PostAsJsonAsync(ApiAdrss, model);
            if (HttpResponse.Result.IsSuccessStatusCode)
            {
                var ReModel = HttpResponse.Result.Content.ReadAsAsync<OutModel>();
                return ReModel.Result;
            }
            else
            {
                //result.IsSuccessful = false;
                //result.Message = string.Format("{0}：{1}", ApiAdrss, HttpResponse.Result.RequestMessage);
                return default(OutModel) ;
            }
        }
    }
}