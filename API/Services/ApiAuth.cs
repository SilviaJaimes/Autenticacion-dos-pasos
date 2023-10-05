using Dominio.Entities;
using TwoFactorAuthNet;
using TwoFactorAuthNet.Providers.Qr;

namespace API.Services;
public class AuthService: IAuthService{
    private readonly IConfiguration _Conf;
    public AuthService(        
        IConfiguration conf ,
        ILogger<AuthService> logger
    ){
        _Conf = conf;        
    }

    public byte[] CreateQR(ref User u){        
        if( u.Email == null){
            throw new ArgumentNullException(u.Email);
        }

        var tfa = new TwoFactorAuth(
            _Conf["JWT:Issuer"],      //* Issuer
            6,                                //* Longitud del codigo
            30,                               //* Duracion de la generacion
            Algorithm.SHA256,                 //* Algoritmo de cifrado
            new ImageChartsQrCodeProvider()   //* Creador del Qr
        );

        string secret = tfa.CreateSecret(160);
        u.Password = secret;

        var QR = tfa.GetQrCodeImageAsDataUri(
            u.Email,            //* El Label a encriptar en el qr
            u.Password   //* Patron secreto
        ); //* Genera la uri del QR

        string UriQR = QR.Replace("data:image/png;base64,", "");

        return Convert.FromBase64String(UriQR); //* Regresamos el qr en froma de bytes
    }

    public bool VerifyCode(string secret, string code){   
        //     
        var tfa = new TwoFactorAuth(_Conf["JWT:Issuer"],6,30,Algorithm.SHA256);
        //? el codigo tiene 1 minuto por defecto hasta que se vence porque posee 30s de discrepancia
        return tfa.VerifyCode( //* valida que el codigo sea generado usando el patron
            secret, //* Patron del Usuario
            code    //* Codigo generado por la aplicaion de autenticacion            
        );
    }

}
