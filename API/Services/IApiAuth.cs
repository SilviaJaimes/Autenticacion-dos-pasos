using Dominio.Entities;

namespace API.Services;
public interface IAuthService{
    byte[] CreateQR(ref User u);
    bool VerifyCode(string secret, string code);
}