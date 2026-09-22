using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace Models.CourseRepository
{
    public class CourseRepository
    {
        private readonly Context _db;

        public CourseRepository(Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<EntityCourse>> GetAllCourseAsync()
        {
            return await _db.Courses
                .Include(c=>c.Author)
                .Include(c=>c.lessonEntities)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<EntityCourse>> SearchCourse(string title)
        {
           return await _db.Courses
                .Where(c => c.Title.Contains(title))
                .AsNoTracking()
                .ToListAsync();

        }
        public async Task<IEnumerable<EntityCourse>> GetByMinPriceAsync(decimal minPrice)
        {
            return await _db.Courses
                .Where( c => c.Price > minPrice)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<int> GetCountCoursAsync()
        {
            return await _db.Courses
                .CountAsync();
        }

        public async Task<EntityCourse> CreateCourse(EntityCourse course)
        {
            course.Id = Guid.NewGuid();
           _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return course;

        }

        public async Task<EntityCourse?> GetByIdAsync(Guid id)
        {
            return await _db.Courses
                .Include(c=>c.Author)
                .Include(c => c.lessonEntities)
                .Include(c => c.Students)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

        }
        public async Task<IEnumerable<EntityCourse>> GetCheapAsync()
        {
            return await _db.Courses
                .Include(c=>c.Author)
                .Include(c => c.lessonEntities)
                .Include(c => c.Students)
                .Where(c =>c.Price>500)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var course = await _db.Courses.FindAsync(id); 
            if (course is null) return false;
            _db.Courses.Remove(course);                       
            await _db.SaveChangesAsync();                               
            return true;
        }

        public async Task<EntityCourse?> UpdateAsync(EntityCourse course)
        {
            
            var existing = await _db.Courses.FindAsync(course.Id);

            
            if (existing is null) return null;

            
            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.Price = course.Price;

            // 4. сохрани
            await _db.SaveChangesAsync();

           
            return existing;
        }



    }
}
