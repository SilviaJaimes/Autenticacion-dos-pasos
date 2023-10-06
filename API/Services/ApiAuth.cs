using API.Dtos;
using Dominio.Entities;
using Microsoft.AspNetCore.Identity;
using TwoFactorAuthNet;
using TwoFactorAuthNet.Providers.Qr;
using Dominio.Interfaces;
using System.Net.Mail;
using System.Net;

namespace API.Services;
public class AuthService: IAuthService{
    private readonly IConfiguration _Conf;
    private readonly IUnitOfWork _unitOfWork;


    public AuthService(IConfiguration conf, IUnitOfWork unitOfWork){
        _Conf = conf;      
        _unitOfWork = unitOfWork;
    }

    public async Task<byte[]> CreateQR(User user,LoginDto data)
    {
        if (user.Email == null)
        {
            throw new ArgumentNullException(user.Email);
        }
        var tfa = new TwoFactorAuth(
            _Conf["JWTSettings:Issuer"],      //* Issuer
            6,                                //* Longitud del codigo
            30,                               //* Duracion de la generacion
            Algorithm.SHA256,                 //* Algoritmo de cifrado
            new ImageChartsQrCodeProvider()   //* Creador del Qr
        );

        string secret = tfa.CreateSecret(160); //* Crea una patron secreto de 160 bites
        user.TwoFactorSecret = secret;

        // Actualiza el usuario en la base de datos
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveAsync(); // Asegúrate de esperar la operación asincrónica

        var QR = tfa.GetQrCodeImageAsDataUri(
            user.Email,            //* El Label a encriptar en el qr
            user.TwoFactorSecret   //* Patron secreto
        ); //* Genera la uri del QR

        string UriQR = QR.Replace("data:image/png;base64,", "");

        return Convert.FromBase64String(UriQR); //* Regresamos el qr en forma de bytes
        
    }

    public async Task  SendQRCodeToEmail(User u,LoginDto data)
    {
        try{
            byte[] qrCode = await CreateQR(u,data);
            var emailRequest = new EmailDTO
            {
                Para = u.Email,                 
                Asunto = "Código QR de autenticación de dos factores",
                Contenido = "Aquí tienes tu código QR de autenticación de dos factores adjunto como un archivo."
            };
            // Adjunta el código QR como un archivo
            emailRequest.ArchivoAdjunto = new Attachment(new MemoryStream(qrCode), "codigo_qr.png");
            // Envía el correo con el código QR adjunto
            SendEmail(emailRequest,qrCode);
        }
        catch (Exception ex){
            Console.WriteLine($"Error al enviar el código QR al correo: {ex.Message}");
        }
    }
    public void SendEmail(EmailDTO request,byte[] qrCode)
    {
        using (var client = new System.Net.Mail.SmtpClient())
        {
            var email = new MailMessage
            {
                From = new MailAddress(_Conf.GetSection("Email:UserName").Value),
                Subject = request.Asunto,
                IsBodyHtml = true,
                Body = request.Contenido
            };
            email.To.Add(request.Para);
            if (request.ArchivoAdjunto != null)
            {
                var attachment = new Attachment(new MemoryStream(qrCode), "codigo_qr.png");
                email.Attachments.Add(attachment); // Adjunta el archivo al correo electrónico
            }
            using (client)
            {
                client.Host = _Conf.GetSection("Email:Host").Value;
                client.Port = Convert.ToInt32(_Conf.GetSection("Email:Port").Value);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential
                (
                    _Conf.GetSection("Email:UserName").Value,
                    _Conf.GetSection("Email:PassWord").Value
                );
                client.Send(email);
            }
        }
    }
    public bool VerifyCode(string secret, string code){   
        var tfa = new TwoFactorAuth(_Conf["JWT:Issuer"],6,30,Algorithm.SHA256);
        return tfa.VerifyCode(secret,code);
    }
}
