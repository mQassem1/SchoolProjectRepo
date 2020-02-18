using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Models.Helpers
{
    public interface ISendSMS
    {
        string SendSMSMessage(string numbers,string message);
    }
}
