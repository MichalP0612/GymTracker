using GymTracker.Api.model;
using GymTracker.Api.enums;

namespace GymTracker.Api.DTOs;

public class ExerciseStatisticsDto
{
    public int ExerciseId {get; set;}
    public string ExerciseName {get; set;} = string.Empty;
    public decimal BestWeight {get; set;}
    public WeightUnit BestWeightUnit {get; set;}
    public int BestWeightReps {get; set;}
    public decimal BestEstimatedOneRepMax {get; set;}
    public decimal TotalVolume {get; set;}
    public List<ExerciseProgressDto> Progress{get; set;} = new();
}

public class ExerciseProgressDto
{
    public DateTime Date {get; set;}
    public decimal EstimatedOneRepMax {get; set;}
}