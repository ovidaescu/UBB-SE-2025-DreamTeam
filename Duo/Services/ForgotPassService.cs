using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Duo.Repositories;
using Duo.Models;

namespace DuolingoNou.Services
{
    public class ForgotPassService
    {
        private readonly UserRepository _userRepository;
        private string _verificationCode;
        private string _userEmail;

        public ForgotPassService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> SendVerificationCode(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                return false;

            _userEmail = email;
            _verificationCode = GenerateVerificationCode();

            return await SendEmail(email, _verificationCode);
        }

        public bool VerifyCode(string enteredCode)
        {
            return enteredCode == _verificationCode;
        }

        public bool ResetPassword(string email, string newPassword)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                return false;

            var updatedUser = new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = newPassword,
                PrivacyStatus = user.PrivacyStatus,
                OnlineStatus = user.OnlineStatus,
                DateJoined = user.DateJoined,
                ProfileImage = user.ProfileImage,
                TotalPoints = user.TotalPoints,
                CoursesCompleted = user.CoursesCompleted,
                QuizzesCompleted = user.QuizzesCompleted,
                Streak = user.Streak
            };

            _userRepository.UpdateUser(updatedUser);
            return true;
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }


        private async Task<bool> SendEmail(string email, string code)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("georgedregan.27@gmail.com", "unyv ykud usjg cvaz"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 20000 // Increased timeout
                })
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("georgedregan.27@gmail.com", "duo"),
                        Subject = "Password Reset Code",
                        Body = $"Your verification code is: {code}",
                        IsBodyHtml = false,
                        Priority = MailPriority.Normal
                    };
                    mailMessage.To.Add(email);

                    // Add headers to improve deliverability
                    mailMessage.Headers.Add("X-Mailer", "YourAppName");

                    await smtpClient.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Error: {smtpEx.StatusCode} - {smtpEx.Message}");
                if (smtpEx.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {smtpEx.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
            return false;
        }
    }
}