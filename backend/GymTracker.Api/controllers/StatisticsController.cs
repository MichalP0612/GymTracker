using GymTracker.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using GymTracker.Api.Services;

namespace GymTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public async Task<ActionResult<StatisticsResponseDto>> GetStatistics()
    {
        var statistics = await _statisticsService.GetStatisticsAsync();

        return Ok(statistics);
    }

    [HttpGet("exercises/{exerciseId}")]
    public async Task<ActionResult<ExerciseStatisticsDto>> GetExerciseStatistics(int exerciseId)
    {
        var statistics = await _statisticsService.GetExerciseStatisticsAsync(exerciseId);

        if(statistics == null)
        {
            return NotFound();
        }

        return Ok(statistics);
    }
}