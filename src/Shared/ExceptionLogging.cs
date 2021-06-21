using System;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ExceptionLogging
    {
        const string APP_NAME = "Organic Wizard";
        const string FROM_EMAIL = "organic.wizard.app@hotmail.com";
        const string FROM_PASSWORD = "&vF7(,s)EL/7@+Md";
        const string TO_EMAIL = "claude.roy791@gmail.com";
        const string SUBJECT = "An Exception Occured";
        const int MAX_LINE_LENGTH = 100;
        public static void EmailError(Exception exception)
        {
            Task.Run(() =>
            {
                try
                {
                    var fromAddress = new MailAddress(FROM_EMAIL, APP_NAME);
                    var toAddress = new MailAddress(TO_EMAIL, APP_NAME);
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Exception occured in {APP_NAME}");
                    builder.AppendLine(string.Empty);
                    AppendLine(builder,$"Source: {exception.Source}");
                    builder.AppendLine(string.Empty);
                    AppendLine(builder, $"Message: {exception.Message}");
                    builder.AppendLine(string.Empty);
                    builder.AppendLine(string.Empty);
                    AppendLine(builder, $"Stack Trace: {exception.StackTrace}");


                    if (exception.InnerException != null)
                    {
                        builder.AppendLine(string.Empty);
                        builder.AppendLine(string.Empty);
                        builder.AppendLine("---------- Child exceptions ----------");
                        builder.AppendLine(string.Empty);
                        while (exception.InnerException != null)
                        {
                            exception = exception.InnerException;
                            builder.AppendLine("---------- Inner exception ----------");
                            AppendLine(builder, $"Source: {exception.Source}");
                            builder.AppendLine(string.Empty);
                            AppendLine(builder, $"Message: {exception.Message}");
                            builder.AppendLine(string.Empty);
                            AppendLine(builder, $"Stack Trace: {exception.StackTrace}");
                            builder.AppendLine("---------- End ----------");
                            builder.AppendLine(string.Empty);
                            builder.AppendLine(string.Empty);
                        }
                    }

                    using (var smtp = new SmtpClient
                    {
                        Host = "smtp.live.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, FROM_PASSWORD),
                        Timeout = 20000
                    })
                    {
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = SUBJECT,
                            Body = builder.ToString()
                        })
                        {
                            smtp.Send(message);
                        }
                    }
                }
                catch { }
            });
        }

        private static void AppendLine(StringBuilder sb, string message, int maxLineLength = MAX_LINE_LENGTH)
        {
            if (string.IsNullOrEmpty(message))
                return;

            int length = 1;
            while (message.Length > 0)
            {
                length = message.Length > maxLineLength ? maxLineLength :message.Length;
                string subMessage = message.Substring(0, length);
                message = message.Remove(0, length);
                sb.AppendLine(subMessage);
            }
        }
    }
}
