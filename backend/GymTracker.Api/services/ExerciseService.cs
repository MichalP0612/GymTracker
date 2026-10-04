using GymTracker.Api.Data;
using GymTracker.Api.model;
using GymTracker.Api.DTOs;
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

    public async Task<Exercise> CreateExerciseAsync(CreateExerciseDto exercise)
    {

        var newExercise = new Exercise
        {
            Name = exercise.Name,
            MuscleGroup = exercise.MuscleGroup,
        };

        _context.Exercises.Add(newExercise);

        await _context.SaveChangesAsync();

        return newExercise;
    }
}