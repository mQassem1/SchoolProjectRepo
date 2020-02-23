using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Models.Helpers
{
    public class SendSMS : ISendSMS
    {
        private readonly IConfiguration config;
        private readonly SMSSettings smsSettings;
        public string result;
        public SendSMS(IConfiguration config,
                       IOptions<SMSSettings> smsSettings)
        {
            this.config = config;
            this.smsSettings = smsSettings.Value;
           
        }

        public string SendSMSMessage(string number,string messag)
        {
            var apiKey = smsSettings.ApiKey;
            string sender = smsSettings.Sender;
            string message = messag;
            string numbers = number;
           
            string url = smsSettings.Url + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }
    }

    public class SMSSettings
    {
        public  string ApiKey { get; set; }
        public string Sender { get; set; }
        public string Url { get; set; }
    }
}
