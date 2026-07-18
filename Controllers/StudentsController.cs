using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

using SudentRepository;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repo;


        public StudentsController(StudentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repo.GetAllStudentsCourseAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _repo.GetByStudentIdAsync(id);
            if (student is null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateById(StudentEntity student)
        {
            var result = await _repo.CreateStudent(student);
            if (student is null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, StudentEntity student)
        {
            student.Id = id;
            var result = await _repo.UpdateStudentAsync(student);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.DeleteStudentAsync(id);
            if (!result) return NotFound();
            
            return NoContent();
        }
        [HttpPost("{studentId}/enroll/{courseId}")]
        public async Task<IActionResult> Enroll(Guid studentId, Guid courseId)
        {
            var result = await _repo.EnrollAsync(studentId, courseId);
            if (!result) return NotFound();
            return NoContent();

        }





    }
}