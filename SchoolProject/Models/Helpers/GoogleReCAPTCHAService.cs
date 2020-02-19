using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SchoolProject.Models.Helpers
{
    public class GoogleReCAPTCHAService
    {
        private readonly ReCAPTCHASettings option;

        public GoogleReCAPTCHAService(IOptions<ReCAPTCHASettings> option)
        {
            this.option = option.Value;
        }

        public virtual async Task<GoogleResponse> ResponseVerification(string token)
        {
            GoogleReCAPTCHAData data = new GoogleReCAPTCHAData
            {
                response = token,
                secret = option.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", data.secret),
                new KeyValuePair<string, string>("response", data.response)
            });
         
            var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify",content);

            string JSONres = response.Content.ReadAsStringAsync().Result;
          

            var capresponse = JsonConvert.DeserializeObject<GoogleResponse>(JSONres);


            return capresponse;
        }
    }

    // ReCAPTCHA Settings App Setting
    public class ReCAPTCHASettings
    {
        public string ReCAPTCHA_Site_Key { get; set; }
        public string ReCAPTCHA_Secret_Key { get; set; }
    }


    //To Hold API  needed Data
    public class GoogleReCAPTCHAData
    {
        public string response { get; set; } //Token
        public string secret { get; set; }
    }


    //API Response 
    public class GoogleResponse
    {
        public bool success { get; set; } 
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }

    
}
