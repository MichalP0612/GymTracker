using GymTracker.Api.Data;
using GymTracker.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Api.Services;

public class StatisticsService : IStatisticsService
{
    private readonly GymTrackerDbContext _context;
    private readonly WeightConverterService _weightConverterService;

    public StatisticsService(GymTrackerDbContext context, WeightConverterService weightConverterService)
    {
        _context = context;
        _weightConverterService = weightConverterService;
    }

    public async Task<StatisticsResponseDto> GetStatisticsAsync()
    {
        // Count workouts
        var totalWorkouts = await _context.Workouts.CountAsync();

        var sets = await _context.WorkoutSets.Include(s => s.Exercise).ToListAsync();

        // Calculate training volume
        var totalVolume = Math.Round(sets.Sum(s => _weightConverterService.ToKilos(s.Weight, s.Unit) * s.Reps), 2);

        var personalRecords = sets.GroupBy(s => s.ExerciseID).Select(group =>
        {
            var bestSetResult = group
            .Select(s => new{Set = s, WeightInKg = _weightConverterService.ToKilos(s.Weight, s.Unit)})
            .OrderByDescending(x => x.WeightInKg * (1 + x.Set.Reps / 30m)).First();

            var estimatedOneRepMax = bestSetResult.WeightInKg * (1 + bestSetResult.Set.Reps / 30m);

            return new PersonalRecordDto
            {
                ExerciseId = bestSetResult.Set.ExerciseID,
                ExerciseName = bestSetResult.Set.Exercise!.Name,
                Weight = bestSetResult.Set.Weight,
                Reps = bestSetResult.Set.Reps,
                Unit = bestSetResult.Set.Unit,
                EstimatedOneRepMax = Math.Round(estimatedOneRepMax, 2)
            };
        }).ToList();

        return new StatisticsResponseDto
        {
            TotalWorkouts = totalWorkouts,
            TotalVolume = totalVolume,
            PersonalRecords = personalRecords
        };
    }

    public async Task<ExerciseStatisticsDto> GetExerciseStatisticsAsync(int exerciseId)
    {
        var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.ID == exerciseId);

        if(exercise == null)
        {
            return null;
        }

        var sets = await _context.WorkoutSets
            .Include(s => s.Workout)
            .Where(s => s.ExerciseID == exerciseId)
            .ToListAsync();

        if(sets.Count == 0)
        {
            return new ExerciseStatisticsDto
            {
                ExerciseId = exercise.ID,
                ExerciseName = exercise.Name,
            };
        }

        var setsWithKg = sets.Select(s => new { Set = s, WeightInKg = _weightConverterService.ToKilos(s.Weight, s.Unit)}).ToList();

        var bestSet = setsWithKg.OrderByDescending(x => x.WeightInKg * (1 + x.Set.Reps / 30m)).First();

        var bestEstimatedOneRepMax = bestSet.WeightInKg * (1 + bestSet.Set.Reps / 30m);

        var totalVolume = Math.Round(setsWithKg.Sum(x => x.WeightInKg * x.Set.Reps), 2);

        var progress = setsWithKg
            .GroupBy(x => x.Set.Workout!.Date.Date)
            .Select(group =>
            {
                var bestDailySet = group.OrderByDescending(x => x.WeightInKg * (1 + x.Set.Reps / 30m)).First();

                var estimatedOneRepMax = bestDailySet.WeightInKg * (1 + bestDailySet.Set.Reps / 30m);

                return new ExerciseProgressDto
                {
                    Date = group.Key,
                    EstimatedOneRepMax = Math.Round(estimatedOneRepMax, 2)
                };
            }).OrderBy(x => x.Date).ToList();

            return new ExerciseStatisticsDto
            {
                ExerciseId = exercise.ID,
                ExerciseName = exercise.Name,
                BestWeight = bestSet.Set.Weight,
                BestWeightUnit = bestSet.Set.Unit,
                BestWeightReps = bestSet.Set.Reps,
                BestEstimatedOneRepMax = Math.Round(bestEstimatedOneRepMax, 2),
                TotalVolume = totalVolume,
                Progress = progress
            };
    }
}