using Microsoft.EntityFrameworkCore;
using Serilog;
using Taskly.Application.Interfaces;
using Taskly.Application.Services;
using Taskly.Infrastructure.Persistence;
using Taskly.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);


// Serilog (loggning till konsol)
Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.WriteTo.Console()
.CreateLogger();


builder.Host.UseSerilog();


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TasklyDbContext>(opt =>
opt.UseSqlServer(connectionString));


builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Taskly.Api.Middleware.ExceptionHandlingMiddleware>();


// TODO JWT läggs i Del 5


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();
app.UseMiddleware<Taskly.Api.Middleware.ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();


app.Run();