using GymTracker.Api.Data;
using GymTracker.Api.model;
using GymTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTracker.Api.DTOs;

namespace GymTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exercise>>> GetExercise()
    {
        return Ok(await _exerciseService.GetExercisesAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Exercise>> CreateExercise(CreateExerciseDto exercise)
    {
        var createdExercise = await _exerciseService.CreateExerciseAsync(exercise);

        return CreatedAtAction(nameof(GetExercise), new {id = createdExercise.ID}, createdExercise);
    }
}