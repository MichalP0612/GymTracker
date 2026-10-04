using GymTracker.Api.enums;
using GymTracker.Api.model;

namespace GymTracker.Api.Services;

public class WeightConverterService
{
   public decimal ToKilos(decimal weight, WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.Kilograms => weight,
            WeightUnit.Pounds => weight * 0.45359237m,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    } 
}

