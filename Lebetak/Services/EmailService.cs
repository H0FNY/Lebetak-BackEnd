using System.Net;
using System.Net.Mail;

namespace Lebetak.Services
{
    public class EmailService
    {
        public void SendMessage(string SendtoEmail, string Subject, string Body)
        {

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("ahofny8@gmail.com", "qtmupjvasunobbvc"),//qtmu pjva suno bbvc
                EnableSsl = true
            };
            client.Send("from@example.com", SendtoEmail, Subject, Body);
        }
    }
}
