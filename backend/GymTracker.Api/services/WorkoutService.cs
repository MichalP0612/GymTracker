using GymTracker.Api.Data;
using GymTracker.Api.DTOs;
using GymTracker.Api.model;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Api.Services;

public class WorkoutService : IWorkoutService
{
    private readonly GymTrackerDbContext _context;

    public WorkoutService(GymTrackerDbContext context)
    {
        _context = context;
    }

    public async Task <IEnumerable<WorkoutResponseDto>> GetWorkoutAsync()
    {
        return await _context.Workouts
            .Include(w => w.Sets)
            .ThenInclude(s => s.Exercise)
            .Select(w => new WorkoutResponseDto
            {
                Id = w.ID,
                Name = w.Name,
                Date = w.Date,
                Set = w.Sets.Select(s => new WorkoutSetResponseDto
                {
                    Id = s.ID,
                    ExerciseId = s.ExerciseID,
                    ExerciseName = s.Exercise!.Name,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    Rpe = s.Rpe
                }).ToList()
            }).ToListAsync();
    }

    public async Task <WorkoutResponseDto?> GeyWorkoutByIdAsync(int ID)
    {
        return await _context.Workouts
            .Include(w => w.Sets)
            .ThenInclude(s => s.Exercise)
            .Select(w => new WorkoutResponseDto
            {
                Id = w.ID,
                Name = w.Name,
                Date = w.Date,
                Set = w.Sets.Select(s => new WorkoutSetResponseDto
                {
                    Id = s.ID,
                    ExerciseId = s.ExerciseID,
                    ExerciseName = s.Exercise!.Name,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    Rpe = s.Rpe
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task <Workout> CreateWorkoutAsync(Workout workout)
    {
        _context.Workouts.Add(workout);

        await _context.SaveChangesAsync();

        return workout;
    }
}