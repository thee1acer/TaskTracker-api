using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using TaskTracker.Common.Models;
namespace TaskTracker.Database.Services.UserAuthentication
{
    public class LoginUser
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<LoginUser> _logger;

        public LoginUser(TaskTrackerContext taskTrackerContext, ILogger<LoginUser> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<ApplicationUserDTO> ExecuteAsync(LoginAuthDataDTO applicationUserLoginDto, CancellationToken cancellationToken = default)
        {
            if (applicationUserLoginDto.Email == default || applicationUserLoginDto.UnhashedPassword == default) return default;

            var applicationUser = await _taskTrackerContext.ApplicationUsers
                .Include(v => v.UserPassword)
                .FirstOrDefaultAsync(v => v.Email == applicationUserLoginDto.Email, cancellationToken)
                    .ConfigureAwait(false);

     
            if (applicationUser == default ) return default;

            var verificationResult = VerifyPassword(applicationUserLoginDto.UnhashedPassword, applicationUser.UserPassword.PasswordHash);

            if (verificationResult)
            {
                var responseData = applicationUser.Adapt<ApplicationUserDTO>();

                return SetLoginToken(responseData);
            }
            return default;
        }

        private static ApplicationUserDTO SetLoginToken(ApplicationUserDTO responseData)
        {
            var token = new Random().Next(10000, 300000).ToString() + DateTime.UtcNow.Second.ToString();

            responseData.Token = token;

            return responseData;
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
