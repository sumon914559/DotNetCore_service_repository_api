using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BLL.Model
{
    public class OcrDataResponse
    { 
        [JsonPropertyName("status")]
        
        public string status { get; set; }

        [JsonPropertyName("status_code")]
        public long status_code { get; set; }

        [JsonProperty("data")]
        public DataDetails Data { get; set; }
       
    }

    public class DataDetails
    {
        [JsonPropertyName("uuid")]
        public string uuid { get; set; }

        [JsonPropertyName("nid_no")]
        public string nid_no { get; set; }

        [JsonPropertyName("dob")]
        public string dob { get; set; }


        [JsonPropertyName("applicant_name_ben")]
        public string applicant_name_ben { get; set; }
        
        [JsonPropertyName("applicant_name_eng")]
        public string applicant_name_eng { get; set; }
        
        [JsonPropertyName("father_name")]
        public string father_name { get; set; }
        
        [JsonPropertyName("mother_name")]
        public string mother_name { get; set; }

        [JsonPropertyName("spouse_name")]
        public string spouse_name { get; set; }

        [JsonPropertyName("address")]
        public string address { get; set; }
    }
}