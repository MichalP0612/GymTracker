using GymTracker.Api.Data;
using GymTracker.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GymTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GymTracker")));

builder.Services.AddScoped<IExerciseService, ExerciseService>();

builder.Services.AddScoped<IWorkoutService, WorkoutService>();

builder.Services.AddControllers();

var app = builder.Build();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();