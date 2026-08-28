using GymTracker.Api.enums;

namespace GymTracker.Api.model;

public class WorkoutSet
{
    public int ID {get; set;}
    public int WorkoutID {get; set;}
    public int ExerciseID {get; set;}
    public decimal Weight {get; set;}
    public WeightUnit Unit { get; set; }
    public int Reps {get; set;}
    public decimal? Rpe {get; set;}
    public Workout? Workout {get; set;}
    public Exercise? Exercise{get; set;}

}