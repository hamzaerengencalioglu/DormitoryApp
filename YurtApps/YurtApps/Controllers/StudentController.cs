using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            try
            {
                await _studentService.CreateStudentAsync(student);
                return Ok("Student succesfully");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok("Student successfully deleted");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var result = await _studentService.GetAllStudentAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await _studentService.GetStudentByIdAsync(id);
            if (result == null)
                return NotFound("Student not found");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            try
            {
                await _studentService.UpdateStudentAsync(student);
                return Ok("Student successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
