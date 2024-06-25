﻿
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Database.Services.Task;

namespace TaskTracker.Controllers.Task
{
    public static class LibraryRegistration
    {
        public static void IntegrateMainServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddTransient<GetAllTasks>();
            service.AddTransient<UpdateTask>();
            service.AddTransient<DeleteTask>();
            service.AddTransient<AddTask>();
        }
    }
}