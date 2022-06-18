using DLL.Repositories;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.Model.Request;
using BLL.Model.Response;
using DLL;
using DLL.Model;
using Microsoft.AspNetCore.Hosting;
using BLL.Model;
using Newtonsoft.Json;

namespace BLL.Services
{
    public interface INidUploadService
    {
        Task<NidUploadResponse> GetNidInfo(NidUploadRequest request);
           
    }

    public class NidUploadService : INidUploadService
    {
       
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;


        public NidUploadService(IHttpClientFactory httpClientFactory, ICustomerInfoRepository customerInfoRepository,IHostingEnvironment environment,ApplicationDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _customerInfoRepository = customerInfoRepository;
            _environment = environment;
            _context = context;
           

        }


        public async Task<NidUploadResponse> GetNidInfo(NidUploadRequest request)
        {
             NidUploadResponse OutputResponse = new NidUploadResponse();

            if (request.nidFont.Length > 0 && request.nidBack.Length > 0)
            {
                var imageName = request.nidFont.FileName;
                var imageExtension = imageName.Split('.', ' ');
                var extension = imageExtension[1];

                //if (extension != "jpg" || extension != "jpeg" || extension != "png")
                //{

                //    OutputResponse.StatusCode = 111;
                //    OutputResponse.Status = "only jpg/jpeg/PNG formats are allowed";
                //    return OutputResponse;
                //}


                try
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                        }
                        if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
                        {
                            _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + "FontImg" + request.nidFont.FileName))
                        {
                            request.nidFont.CopyTo(filestream);
                            filestream.Flush();
                            //return "\\uploads\\" + files.nidFont.FileName;
                        }
                        using (FileStream filestream1 = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + "BackImg" + request.nidBack.FileName))
                        {
                            request.nidFont.CopyTo(filestream1);
                            filestream1.Flush();
                            //  return "\\uploads\\" + request.nidBack.FileName;
                        }

                    var dummyOcrRe = new DummyNidOcrRequest()
                    {
                        id_front = "FontImg" + request.nidFont.FileName,
                        id_back = "BackImg" + request.nidBack.FileName
                    };



                        var res = await GetOcrResResult(dummyOcrRe);

                        OutputResponse.StatusCode = 200;
                        OutputResponse.Status = "Success";
                        OutputResponse.BnName = res.Data.applicant_name_ben;
                        OutputResponse.EnName = res.Data.applicant_name_eng;
                        OutputResponse.DoB = res.Data.dob;
                        OutputResponse.NidNumber = res.Data.nid_no;
                        OutputResponse.FatherName = res.Data.father_name;
                        OutputResponse.MotherName = res.Data.mother_name;
                        OutputResponse.Address = res.Data.address;
                        


                    var dbInsertModel = new CustomerInfo()
                    {
                        Uid = res.Data.uuid,
                        CustomerName = res.Data.applicant_name_eng,
                        CustomerNameBen = res.Data.applicant_name_ben,
                        NIDNumber = res.Data.nid_no,
                        DateOfBirth =res.Data.dob,
                        FatherName = res.Data.father_name,
                        SpouseName = res.Data.spouse_name,
                        MotherName = res.Data.mother_name,
                        Address = res.Data.address,
                    };

                    await _customerInfoRepository.CreateAsync(dbInsertModel);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return OutputResponse;
                    }

                    OutputResponse.StatusCode = 110;
                        OutputResponse.Status = "Data insert problem";
                        return OutputResponse;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error", ex);
                    }
                }
          

            else
            {
                OutputResponse.StatusCode = 100;
                OutputResponse.Status = "Fail";
                return OutputResponse;
            }
        }


        private async Task<OcrDataResponse> GetOcrResResult(DummyNidOcrRequest ocrRequest)
        {

            OcrDataResponse resultData = new OcrDataResponse();
            try
            {
              
                var client = _httpClientFactory.CreateClient();
                var requestData = new StringContent(JsonConvert.SerializeObject(ocrRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:44368/api/ocr/dummyNidocr", requestData);
                var res = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return  JsonConvert.DeserializeObject<OcrDataResponse>(res);
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

            return resultData;


        }


    }
}
