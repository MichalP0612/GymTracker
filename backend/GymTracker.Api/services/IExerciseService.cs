using GymTracker.Api.model;

namespace GymTracker.Api.Services;

public interface IExerciseService
{
    Task<IEnumerable<Exercise>> GetExercisesAsync();

    Task<Exercise> CreateExerciseAsync(Exercise exercise);
}