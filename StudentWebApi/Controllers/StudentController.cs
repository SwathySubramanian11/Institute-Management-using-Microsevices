using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using StudentLibrary.Models;
using StudentLibrary.Repos;
using System.Text;

namespace StudentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        readonly IEFStudentRepoAsync studentRepo;
        public StudentController(IEFStudentRepoAsync studentRepoAsync)
        {
            studentRepo = studentRepoAsync;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudents()
        {
            List<Student> students = await studentRepo.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{rno}")]
        public async Task<ActionResult> GetOne(string rno)
        {
            try
            {
                Student student = await studentRepo.GetStudentAsync(rno);
                return Ok(student);
            }
            catch (StudentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        private void PublishToMessageQueue(string integrationevent, string eventData)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "studentxchange", routingKey: integrationevent, basicProperties: null, body: body);
        }
        [HttpPost]
        public async Task<ActionResult> Add(Student student)
        {
            try
            {
                await studentRepo.InsertStudentAsync(student);
                return Created($"api/Student/{student.RollNo}", student);
            }
            catch (StudentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{rno}")]
        public async Task<ActionResult> Update(string rno, Student student)
        {
            try
            {
                await studentRepo.UpdateStudentAsync(rno, student);
                return Ok(student);
            }
            catch (StudentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{rno}")]
        public async Task<ActionResult> Delete(string rno)
        {
            try
            {
                await studentRepo.DeleteStudentAsync(rno);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByBatchCode/{bc}")]
        public async Task<ActionResult> GetByBatchCode(string bc)
        {
            try
            {
                List<Student> students = await studentRepo.GetStudentsByBatchCodeAsync(bc);
                return Ok(students);
            }
            catch (StudentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Batch")]
        public async Task<ActionResult> InsertBatch(Batch batch)
        {
            try
            {
                await studentRepo.InsertBatchAsync(batch);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
