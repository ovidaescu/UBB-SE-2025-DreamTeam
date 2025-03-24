using Duo.Repositories;
using Duo.Services;
using DuolingoNou.Services;
using System.Threading.Tasks;

namespace Duo.ViewModels
{
    public class ResetPassViewModel
    {
        private readonly ForgotPassService _forgotPassService;

        public string Email { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public bool IsCodeVerified { get; private set; } = false;

        public ResetPassViewModel(UserRepository userRepository)
        {
            _forgotPassService = new ForgotPassService(userRepository);
        }

        public async Task<bool> SendVerificationCode(string email)
        {
            Email = email;
            return await _forgotPassService.SendVerificationCode(email);
        }

        public bool VerifyCode(string code)
        {
            IsCodeVerified = _forgotPassService.VerifyCode(code);
            return IsCodeVerified;
        }

        public bool ResetPassword(string newPassword)
        {
            if (NewPassword != ConfirmPassword)
            {
                StatusMessage = "Passwords don't match!";
                return false;
            }

            return _forgotPassService.ResetPassword(Email, newPassword);
        }
    }
}