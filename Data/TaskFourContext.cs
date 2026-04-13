
using Microsoft.EntityFrameworkCore;

namespace TASK_4_CSHARP.Data
{
    public class TaskFourContext : DbContext
    {
        public TaskFourContext(DbContextOptions<TaskFourContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}
