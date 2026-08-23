using GymTracker.Api.model;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Api.Data;

public class GymTrackerDbContext : DbContext
{
    public GymTrackerDbContext(DbContextOptions <GymTrackerDbContext> options) : base(options)
    {}
        public DbSet<Workout> Workouts => Set<Workout>();
        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();
}
