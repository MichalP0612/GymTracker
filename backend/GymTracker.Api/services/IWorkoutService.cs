using GymTracker.Api.DTOs;
using GymTracker.Api.model;

namespace GymTracker.Api.Services;

public interface IWorkoutService
{
    Task <IEnumerable<WorkoutResponseDto>> GetWorkoutAsync ();
    Task <WorkoutResponseDto?> GeyWorkoutByIdAsync(int ID);
    Task <Workout> CreateWorkoutAsync(Workout workout);
}