using GymTracker.Api.enums;
using GymTracker.Api.model;

namespace GymTracker.Api.DTOs;

public class StatisticsResponseDto
{
    public int TotalWorkouts { get; set; }
    public decimal TotalVolume { get; set; }
    public List<PersonalRecordDto> PersonalRecords { get; set; } = new();
}

public class PersonalRecordDto 
{
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public int Reps { get; set; }

    public WeightUnit Unit { get; set; }
    public decimal EstimatedOneRepMax { get; set; }
}