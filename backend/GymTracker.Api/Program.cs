using GymTracker.Api.Data;
using GymTracker.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GymTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GymTracker")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IExerciseService, ExerciseService>();

builder.Services.AddScoped<IWorkoutService, WorkoutService>();

builder.Services.AddScoped<IStatisticsService, StatisticsService>();

builder.Services.AddScoped<WeightConverterService>();


var app = builder.Build();

app.UseCors("ReactApp");

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();