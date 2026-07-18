using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<EntityCourse> Courses { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }

}
