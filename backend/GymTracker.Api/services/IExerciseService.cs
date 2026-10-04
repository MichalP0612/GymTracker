using GymTracker.Api.DTOs;
using GymTracker.Api.model;

namespace GymTracker.Api.Services;

public interface IExerciseService
{
    Task<IEnumerable<Exercise>> GetExercisesAsync();

    Task<Exercise> CreateExerciseAsync(CreateExerciseDto exercise);
}