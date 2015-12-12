using System;
namespace TheWorldPracticeAppASPNET5
{
    public interface ImailService
    {

        bool SendMail(string to, string from, string subject, string body);


    }



}
