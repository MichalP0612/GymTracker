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

    public async Task<IEnumerable<WorkoutResponseDto>> GetWorkoutAsync()
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
                    Unit = s.Unit,
                    Reps = s.Reps,
                    Rpe = s.Rpe
                }).ToList()
            }).ToListAsync();
    }

    public async Task<WorkoutResponseDto?> GetWorkoutByIdAsync(int ID)
    {
        return await _context.Workouts
            .Where(w => w.ID == ID)
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
                    Unit= s.Unit,
                    Reps = s.Reps,
                    Rpe = s.Rpe
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<WorkoutResponseDto> CreateWorkoutAsync(CreateWorkoutDto dto)
    {
        var workout = new Workout
        {
            Name = dto.Name,
            Date = dto.Date
        };

        foreach (var setDto in dto.Sets)
        {
            var exerciseExists = await _context.Exercises.AnyAsync(e => e.ID == setDto.ExerciseID);

            if (!exerciseExists)
            {
                throw new ArgumentException($"Exercise with ID {setDto.ExerciseID} does not exist.");
            }

            workout.Sets.Add(new WorkoutSet
            {
                ExerciseID = setDto.ExerciseID,
                Weight = setDto.Weight,
                Unit = setDto.Unit,
                Reps = setDto.Reps,
                Rpe = setDto.Rpe
            });
        }

        _context.Workouts.Add(workout);

        await _context.SaveChangesAsync();

        return await _context.Workouts
            .Include(w => w.Sets)
            .ThenInclude(s => s.Exercise)
            .Where(w => w.ID == workout.ID)
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
                    Unit = s.Unit,
                    Reps = s.Reps,
                    Rpe = s.Rpe
                }).ToList()
            }).FirstAsync();
    }

    public async Task<WorkoutResponseDto?> UpdateWorkoutAsync(int id, UpdateWorkoutDto dto) 
    {
        var workout = await _context.Workouts.Include(w => w.Sets).FirstOrDefaultAsync(w => w.ID == id);

        if (workout == null)
        {
            return null;
        }

        foreach (var setDto in dto.Sets)
        {
            var exerciseExists = await _context.Exercises.AnyAsync(e => e.ID == setDto.ExerciseID);

            if (!exerciseExists)
            {
                throw new ArgumentException($"Exercise with ID {setDto.ExerciseID} does not exist.");
            }
        }

        workout.Name = dto.Name;
        workout.Date = dto.Date;

        _context.WorkoutSets.RemoveRange(workout.Sets);

        workout.Sets.Clear();

        foreach (var setDto in dto.Sets)
        {
            workout.Sets.Add(new WorkoutSet
            {
                ExerciseID = setDto.ExerciseID,
                Weight = setDto.Weight,
                Unit = setDto.Unit,
                Reps = setDto.Reps,
                Rpe = setDto.Rpe
            });
        }

        await _context.SaveChangesAsync();

        return await GetWorkoutByIdAsync(id);
    }

    public async Task<bool> DeleteWorkoutAsync(int id)
    {
        var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.ID == id);

        if (workout == null)
        {
            return false;
        }

        _context.Workouts.Remove(workout);

        await _context.SaveChangesAsync();

        return true;
    }
}
