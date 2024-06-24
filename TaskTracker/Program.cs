using TaskTracker.Database;
using TaskTracker.Database.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// database configuration 
builder.Services.AddOptions<ConnectionDetails>().BindConfiguration("REFERENCE_DB");

builder.Services.AddDbContext<TaskTrackerContext>(
    (provider, options) =>
        {
            var connectionDetails = provider.GetRequiredService<IOptions<ConnectionDetails>>();
            var connectionString = ConnectionStringHelper.BuildConnectionString(connectionDetails.Value);

            options.UseSqlServer(connectionString, ops => ops.EnableRetryOnFailure());
        }
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskTrackerContext>();

    context.EnsureMigrationIsApplied(app.Environment.IsDevelopment());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
