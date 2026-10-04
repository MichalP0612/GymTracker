using GymTracker.Api.DTOs;
using GymTracker.Api.model;

namespace GymTracker.Api.Services;

public interface IWorkoutService
{
    Task <IEnumerable<WorkoutResponseDto>> GetWorkoutAsync ();
    Task <WorkoutResponseDto?> GetWorkoutByIdAsync(int ID);
    Task <WorkoutResponseDto> CreateWorkoutAsync(CreateWorkoutDto workout);
    Task <Workout> UpdateWorkoutAsync(int id, UpdateWorkoutDto workout);
    Task <bool> DeleteWorkoutAsync(int id);
}