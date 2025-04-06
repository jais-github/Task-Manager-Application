using Microsoft.EntityFrameworkCore;
using TaskManager.API.Features.Auth;
using TaskManager.Features.Tags;
using TaskManager.Features.Tasks;

namespace TaskManager.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many Task ↔ Tag
            modelBuilder.Entity<TaskItem>()
        .HasMany(t => t.Tags)
        .WithMany(t => t.Tasks)
        .UsingEntity(j => j.ToTable("TaskTags"));
        }
    }
}
