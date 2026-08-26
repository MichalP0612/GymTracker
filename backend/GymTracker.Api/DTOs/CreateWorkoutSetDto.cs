using System.ComponentModel.DataAnnotations;

namespace GymTracker.Api.DTOs;

public class CreateWorkoutSetDto
{
    [Range (1, int.MaxValue)]
    public int ExerciseID { get; set; }

    [Range (0.00, 1000)]
    public decimal Weight { get; set; }

    [Range (0, 50)]
    public int Reps { get; set; }

    [Range(0.5, 10)]
    public decimal? Rpe { get; set; }
}