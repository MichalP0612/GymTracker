using GymTracker.Api.Data;
using GymTracker.Api.model;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Api.Services;

public class ExerciseService : IExerciseService
{
    private readonly GymTrackerDbContext _context;

    public ExerciseService(GymTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Exercise>> GetExercisesAsync()
    {
        return await _context.Exercises.ToListAsync();
    }

    public async Task<Exercise> CreateExerciseAsync(Exercise exercise)
    {
        _context.Exercises.Add(exercise);

        await _context.SaveChangesAsync();

        return exercise;
    }
}