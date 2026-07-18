using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace SudentRepository
{
    public class StudentRepository
    {
        private readonly Context _db;

        public StudentRepository(Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StudentEntity>> GetAllStudentsCourseAsync()
        {
            return await _db.Students
                
                .Include(c => c.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<StudentEntity> CreateStudent(StudentEntity student)
        {
            student.Id = Guid.NewGuid();
            _db.Students.Add(student);
            _db.SaveChangesAsync();
            return student;

        }

        public async Task<StudentEntity?> GetByStudentIdAsync(Guid id)
        {
            return await _db.Students
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

        }
        

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student is null) return false;
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<StudentEntity?> UpdateStudentAsync(StudentEntity student)
        {

            var existing = await _db.Students.FindAsync(student.Id);


            if (existing is null) return null;


            existing.Name = student.Name;
            

            // 4. сохрани
            await _db.SaveChangesAsync();


            return existing;
        }

        public async Task<bool> EnrollAsync(Guid studentId, Guid courseId)
        {
            
            var student = await _db.Students
    .Include(s => s.Courses)
    .FirstOrDefaultAsync(s => s.Id == studentId);

           
            var course = await _db.Courses.FindAsync(courseId);

            
            
            if(course is null) return false;
            if (student is null) return false;
            

            student.Courses.Add(course);
          
            await _db.SaveChangesAsync();
            

            return true;
        }



    }
}
