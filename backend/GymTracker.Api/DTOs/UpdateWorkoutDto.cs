using System.ComponentModel.DataAnnotations;
 namespace GymTracker.Api.DTOs;

public class UpdateWorkoutDto 
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MinLength(1)]
    public List<CreateWorkoutSetDto> Sets { get; set; } = new();
}