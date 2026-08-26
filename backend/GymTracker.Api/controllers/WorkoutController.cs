using GymTracker.Api.Data;
using GymTracker.Api.model;
using GymTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTracker.Api.DTOs;

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
    public async Task<ActionResult<Workout>> CreateWorkout(CreateWorkoutDto workout)
    {
        try
        {
            var createdWorkout = await _workoutService.CreateWorkoutAsync(workout);
            return CreatedAtAction(nameof(GetWorkout), new {id = createdWorkout.ID}, createdWorkout);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Workout>> UpdateWorkout(int id, UpdateWorkoutDto workout) 
    {
        try 
        {
            var updatedWorkout = await _workoutService.UpdateWorkoutAsync(id, workout);

            if (updatedWorkout == null)
            {
                return NotFound();
            }

            return Ok(updatedWorkout);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        var deleted = await _workoutService.DeleteWorkoutAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}