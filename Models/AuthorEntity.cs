namespace WebApplication1.Models
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityCourse? Course { get; set; }


    }
    public class StudentEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<EntityCourse> Courses { get; set; } = [];
    }
    public class EntityCourse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public List<LessonEntity> lessonEntities { get; set; } = [];
        public AuthorEntity? Author { get; set; }
        public List<StudentEntity> Students { get; set; } = [];
        public Guid AuthorId { get; set; }

    }
    public class LessonEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public Guid CouresID { get; set; }
        public EntityCourse? Course { get; set; }
    }
}
