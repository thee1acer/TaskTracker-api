using Mapster;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using TaskTracker.Common.Models;
using TaskTracker.Database.Models;
using TaskTracker.Database.Services.AppplicationUser;

namespace TaskTracker.Database.Services.UserAuthentication
{
    public class RegisterUser
    {
        private TaskTrackerContext _taskTrackerContext;
        private AddUser _addUserService;
        public ILogger<RegisterUser> _logger;

        public RegisterUser(TaskTrackerContext taskTrackerContext, AddUser addUserService, ILogger<RegisterUser> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _addUserService = addUserService;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(ApplicationUserDTO applicationUserDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var addUserResult = await _addUserService.ExecuteAsync(applicationUserDto, cancellationToken);

                if (!addUserResult) return false;

                var hashedPassword = HashPassword(applicationUserDto.UnhashedPassword);

                var applicationUserPass = applicationUserDto.Adapt<ApplicationUserPassword>();


                await _taskTrackerContext.ApplicationUserPasswords.AddAsync(applicationUserPass, cancellationToken)
                        .ConfigureAwait(false);

                await _taskTrackerContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogDebug("Failed to register user with exception: ", ex);
                return false;
            };
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
