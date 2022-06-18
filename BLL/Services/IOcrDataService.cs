
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;
using BLL.Model.Request;
using BLL.Model.Response;
using DLL;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace BLL.Services
{
    public interface IOcrDataService
    {
        Task<OcrDataResponse> GetNidData(DummyNidOcrRequest request);
           
    }

    public class OcrDataService : IOcrDataService
    {
       
        private readonly IHostingEnvironment _environment;
       


        public OcrDataService(IHostingEnvironment environment)
        {
           
            _environment = environment;
            
        }


        public async Task<OcrDataResponse> GetNidData(DummyNidOcrRequest request)
        {
             OcrDataResponse ResData = new OcrDataResponse();
            if (request.id_front != "" && request.id_front != "")
            { 
             string contentPath = this._environment.ContentRootPath;

                var path = Path.Combine(contentPath, "uploads", request.id_front);
                var path1 = Path.Combine(contentPath, "uploads", request.id_back);
               


                using (StreamReader r = new StreamReader((contentPath+ "/dummyData.json")))
             {
                 string json = r.ReadToEnd();                
                 return JsonConvert.DeserializeObject<OcrDataResponse>(json);                 
             }
               
            }
            else
            {
                ResData.status = "Fail";
                ResData.status_code = 100;

                return ResData;
            }
            return ResData;
        }

        
    }
}
