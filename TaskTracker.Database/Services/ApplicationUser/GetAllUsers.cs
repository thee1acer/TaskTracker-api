using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTracker.Common.Models;

namespace TaskTracker.Database.Services.AppplicationUser
{
    public class GetAllUsers
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<GetAllUsers> _logger;

        public GetAllUsers(TaskTrackerContext taskTrackerContext, ILogger<GetAllUsers> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<List<ApplicationUserDTO>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var applicationUsers =  await _taskTrackerContext.ApplicationUsers.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

            var applicationUsersDto = applicationUsers.Adapt<List<ApplicationUserDTO>>();

            applicationUsersDto.ForEach(applicationUser => applicationUser.UserPassword = null);

            return applicationUsersDto;
        }
    }
}
