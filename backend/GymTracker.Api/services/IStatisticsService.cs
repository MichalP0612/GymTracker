using GymTracker.Api.DTOs;

namespace GymTracker.Api.Services;

public interface IStatisticsService
{
    Task<StatisticsResponseDto> GetStatisticsAsync();
}