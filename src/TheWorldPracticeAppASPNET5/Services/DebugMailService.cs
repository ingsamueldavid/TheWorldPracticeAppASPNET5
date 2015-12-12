using System;
using System.Diagnostics;
namespace TheWorldPracticeAppASPNET5
{
    public class DebugMailService : ImailService
    {

        //bool SendMail(string to, string from, string subject, string body){}
        public bool SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail: To {to}, Subject: {subject}");
            return true;
        }
    }



}
