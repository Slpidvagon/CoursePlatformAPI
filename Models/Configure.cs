using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Course)
                .WithOne(c => c.Author)
                .HasForeignKey<EntityCourse>(c => c.AuthorId);
        }
    }
    public class CourseConfiguration : IEntityTypeConfiguration<EntityCourse>
    {
        public void Configure(EntityTypeBuilder<EntityCourse> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.lessonEntities)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CouresID);

            builder.HasMany(c => c.Students)
                .WithMany(s => s.Courses);
        }
    }
}
