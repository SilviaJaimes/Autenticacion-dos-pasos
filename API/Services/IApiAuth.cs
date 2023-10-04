using Dominio.Entities;

namespace VerificationProject.Services;
public interface IAuthService{
    byte[] CreateQR(ref User u);
    bool VerifyCode(string secret, string code);
}