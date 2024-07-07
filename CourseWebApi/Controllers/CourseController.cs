using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.Models;
using CourseLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CourseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        readonly IEFCourseRepoAsync courseRepo;
        public CourseController(IEFCourseRepoAsync courseRepoAsync)
        {
            courseRepo = courseRepoAsync;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCourses()
        {
            List<Course> courses = await courseRepo.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{cc}")]
        public async Task<ActionResult> GetOne(string cc)
        {
            try
            {
                Course course = await courseRepo.GetCourseAsync(cc);
                return Ok(course);
            }
            catch (CourseException ex)
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
            channel.BasicPublish(exchange: "coursexchange", routingKey: integrationevent, basicProperties: null, body: body);
        }
        [HttpPost("{token}")]
        public async Task<ActionResult> Add(string token,Course course)
        {
            try
            {
                await courseRepo.InsertCourseAsync(course);
                HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5263/api/Batch/") };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsJsonAsync("Course", new { CourseCode = course.CourseCode });
                return Created($"api/Course/{course.CourseCode}", course);
            }
            catch (CourseException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{cc}")]
        public async Task<ActionResult> Update(string cc, Course course)
        {
            try
            {
                await courseRepo.UpdateCourseAsync(cc, course);
                return Ok(course);
            }
            catch (CourseException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cc}")]
        public async Task<ActionResult> Delete(string cc)
        {
            try
            {
                await courseRepo.DeleteCourseAsync(cc);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
