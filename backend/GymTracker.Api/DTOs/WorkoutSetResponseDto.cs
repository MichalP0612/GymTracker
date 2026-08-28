using GymTracker.Api.enums;

namespace GymTracker.Api.DTOs;

public class WorkoutSetResponseDto
{
    public int Id {get; set;}
    public int ExerciseId {get; set;}
    public string ExerciseName {get; set;} = string.Empty;
    public decimal Weight {get; set;}
    public WeightUnit Unit { get; set;}
    public int Reps {get; set;}
    public decimal? Rpe {get; set;}
}