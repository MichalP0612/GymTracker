namespace GymTracker.Api.model;

public class Workout
{
    public int ID {get; set;}
    public string Name {get; set;} = string.Empty;
    public DateTime Date {get; set;}
    public List<WorkoutSet> Sets {get; set;} = new();

}