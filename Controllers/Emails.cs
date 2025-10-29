namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using TodoApi.DTOs;



[Route("api/[controller]")]
[ApiController]
public class Emails : ControllerBase
{
  [HttpPost(Name = "SendEmail")]
  private bool IsValidEmail(string email)
  {
    try
    {
      var addr = new MailAddress(email);
      return addr.Address == email;
    }
    catch
    {
      return false;
    }
  }
  public ActionResult<ResponseAPI> SendEmail(EmailsDTO emailsDTO)
  {
    try
    {
      if (string.IsNullOrEmpty(emailsDTO.ToEmail) || string.IsNullOrEmpty(emailsDTO.Subject) || string.IsNullOrEmpty(emailsDTO.Body))
      {
        return BadRequest(new ResponseAPI { Message = "Todos los campos son obligatorios", StatusCode = 400 });
      }
      if (!IsValidEmail(emailsDTO.ToEmail))
      {
        return BadRequest(new ResponseAPI { Message = "El formato del correo electrónico es inválido", StatusCode = 400 });
      }
      // Configure SMTP client
      SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
      smtpClient.Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("SmtpUser"), Environment.GetEnvironmentVariable("SmtpPass"));
      smtpClient.EnableSsl = true;
      // Create email message
      MailMessage mail = new MailMessage();
      mail.From = new MailAddress(Environment.GetEnvironmentVariable("SmtpUser"), "Sistema de Notificaciones");
      mail.To.Add(new MailAddress(emailsDTO.ToEmail, "Correo de prueba"));
      mail.Subject = emailsDTO.Subject;
      mail.Body = emailsDTO.Body;
      mail.IsBodyHtml = true;
      // Send email
      smtpClient.Send(mail);
      return Ok(new ResponseAPI { Message = "Correo enviado correctamente", StatusCode = 200 });
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error al enviar el correo: {ex.Message}");
      return StatusCode(500, new ResponseAPI { Message = "Ocurrió un error al enviar el correo", StatusCode = 500, ErrorMessage = ex.Message });
    }

  }
}
/*
Template HTML en una sola línea para usar en el body JSON de Postman:

"<!DOCTYPE html><html lang='es'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Correo de TodoApi</title><link href='https://fonts.googleapis.com/css?family=Roboto:400,700&display=swap' rel='stylesheet'><style>body { font-family: 'Roboto', Arial, sans-serif; background-color: #f4f6f8; margin: 0; padding: 0; } .container { max-width: 600px; margin: 40px auto; background: #fff; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.07); padding: 32px; } h1 { color: #2d3748; font-size: 2em; margin-bottom: 16px; } p { color: #4a5568; font-size: 1.1em; line-height: 1.6; } .footer { margin-top: 32px; font-size: 0.9em; color: #a0aec0; text-align: center; } a.button { display: inline-block; padding: 10px 24px; background: #3182ce; color: #fff; border-radius: 4px; text-decoration: none; font-weight: bold; margin-top: 24px; } .image-preview { margin: 24px 0; text-align: center; } .image-preview img { max-width: 100%; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,0.10); }</style></head><body><div class='container'><h1>Correo de TodoApi</h1><p>Este es un mensaje de ejemplo enviado desde TodoApi.</p><div class='image-preview'><img src='https://www.google.com/images/branding/googlelogo/2x/googlelogo_light_color_92x30dp.png' alt='Google Logo'></div><p>Puedes visitar <a href='https://www.google.com/' target='_blank'>Google</a> para buscar información.</p><a href='https://www.google.com/' class='button' target='_blank'>Ir a Google</a><div class='footer'>&copy; 2024 TodoApi. Todos los derechos reservados.</div></div></body></html>"
*/

