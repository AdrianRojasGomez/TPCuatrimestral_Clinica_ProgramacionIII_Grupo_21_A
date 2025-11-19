using System.Net.Mail;
using System.Net;
using System;

namespace Negocio
{
    public class EmailService
    {
        private SmtpClient servidor;
        private MailMessage email;

        public EmailService()
        {
            // Configuración del servidor SMTP 
            servidor = new SmtpClient();
            servidor.Credentials = new NetworkCredential("b9aff267e2ad87", "f491945f94e658");
            servidor.EnableSsl = true;
            servidor.Port = 2525;
            servidor.Host = "sandbox.smtp.mailtrap.io";
        }

        public void EnviarEmail(string emailDestino, string asunto, string cuerpoHtml)
        {
            try
            {
                email = new MailMessage();
                email.From = new MailAddress("no-responder@clinica.com", "Clínica Médica AppUTN");

                // destinatario
                email.To.Add(emailDestino);

                email.Subject = asunto;
                email.IsBodyHtml = true;
                email.Body = cuerpoHtml;

                servidor.Send(email);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al enviar el correo: " + ex.Message, ex);
            }
            finally
            {
                email?.Dispose();
            }
        }
    }
}