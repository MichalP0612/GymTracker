using GymTracker.Api.Data;
using GymTracker.Api.model;
using GymTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workout>>> GetWorkout()
    {
        return Ok(await _workoutService.GetWorkoutAsync());
    }

    [HttpGet("{id}")]
    public async Task <ActionResult<Workout>> GetWorkout(int id)
    {
        var workout = await _workoutService.GeyWorkoutByIdAsync(id);

        if (workout == null)
        {
            return NotFound();
        }

        return Ok(workout);
    }

    [HttpPost]
    public async Task<ActionResult<Workout>> CreateWorkout(Workout workout)
    {
        var createdWorkout = await _workoutService.CreateWorkoutAsync(workout);

        return CreatedAtAction(nameof(GetWorkout), new {id = createdWorkout.ID}, createdWorkout);
    }
}