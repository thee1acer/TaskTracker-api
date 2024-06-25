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

        public async Task<bool> ExecuteAsync(ApplicationUserDTO applicationUserDto, CancellationToken cancellationToken = default)
        {
            var applicationUserId = applicationUserDto.Id;

            var applicationUser = await _taskTrackerContext.ApplicationUsers.FirstOrDefaultAsync(v => v.Id == applicationUserId, cancellationToken)
                    .ConfigureAwait(false);

            var applicationUserPassword = await _taskTrackerContext.ApplicationUserPasswords.FirstOrDefaultAsync(v => v.ApplicationUser.Id == applicationUserId, cancellationToken)
                   .ConfigureAwait(false);

            if (applicationUser == default || applicationUserPassword == default) return false;

            var verificationResult = VerifyPassword(applicationUserDto.UnhashedPassword, applicationUserPassword.PasswordHash);

            return verificationResult;
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
