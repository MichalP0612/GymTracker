using GymTracker.Api.Data;
using GymTracker.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Api.Services;

public class StatisticsService : IStatisticsService
{
    private readonly GymTrackerDbContext _context;

    public StatisticsService(GymTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<StatisticsResponseDto> GetStatisticsAsync()
    {
        // Count workouts
        var totalWorkouts = await _context.Workouts.CountAsync();

        var sets = await _context.WorkoutSets.Include(s => s.Exercise).ToListAsync();

        // Calculate training volume
        var totalVolume = sets.Sum(s => s.Weight * s.Reps);

        var personalRecords = sets.GroupBy(s => s.ExerciseId).Select(group =>
        {
            var bestSet = group.OrderByDescending(s => s.Weight * (1 + s.Reps / 30m)).First();

            var estimatedOneRepMax = bestSet.Weight * (1 + bestSet.Reos / 30m);

            return new PersonRecordDto
            {
                ExerciseId = bestSet.ExerciseId,
                ExerciseName = bestSet.ExerciseName,
                Weight = bestSet.Weight,
                Reps = bestSet.Reps,
                EstimatedOneRepMax = Math.Round(estimatedOneRepMax, 2)
            };
        }).ToList();

        return new StatisticsResponseDto
        {
            TotalWorkouts = totalWorkouts,
            totalVolume = totalVolume,
            personalRecords = personalRecords
        }
    }
}