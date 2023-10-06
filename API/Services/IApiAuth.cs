using API.Dtos;
using Dominio.Entities;

namespace API.Services;
public interface IAuthService{
    Task<byte[]> CreateQR(User user,LoginDto data);
    bool VerifyCode(string secret, string code);
    Task SendQRCodeToEmail(User u, LoginDto data);
    void SendEmail(EmailDTO request,byte[] qrCode);
}