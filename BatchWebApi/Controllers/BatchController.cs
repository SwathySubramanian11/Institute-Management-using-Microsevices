using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BatchLibrary.Models;
using BatchLibrary.Repos;
using RabbitMQ.Client;
using System.Text;

namespace BatchWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BatchController : ControllerBase
    {
        readonly IEFBatchRepoAsync batchRepo;
        public BatchController(IEFBatchRepoAsync batchRepoAsync)
        {
            batchRepo = batchRepoAsync;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBatches()
        {
            List<Batch> batches = await batchRepo.GetAllBatchesAsync();
            return Ok(batches);
        }

        [HttpGet("{bc}")]
        public async Task<ActionResult> GetOne(string bc)
        {
            try
            {
                Batch batch = await batchRepo.GetBatchAsync(bc);
                return Ok(batch);
            }
            catch (BatchException ex)
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
            channel.BasicPublish(exchange: "batchxchange", routingKey: integrationevent, basicProperties: null, body: body);
        }

        [HttpPost("{token}")]
        public async Task<ActionResult> Add(string token,Batch batch)
        {
            try
            {
                await batchRepo.InsertBatchAsync(batch);
                HttpClient client = new HttpClient { BaseAddress=new Uri("http://localhost:5030/api/Student/") };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsJsonAsync("Batch", new { BatchCode = batch.BatchCode });
                return Created($"api/Batch/{batch.BatchCode}", batch);
            }
            catch (BatchException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{bc}")]
        public async Task<ActionResult> Update(string bc, Batch batch)
        {
            try
            {
                await batchRepo.UpdateBatchAsync(bc, batch);
                return Ok(batch);
            }
            catch (BatchException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{bc}")]
        public async Task<ActionResult> Delete(string bc)
        {
            try
            {
                await batchRepo.DeleteBatchAsync(bc);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByCourseCode/{cc}")]
        public async Task<ActionResult> GetByCC(string cc)
        {
            try
            {
                List<Batch> batches = await batchRepo.GetBatchesByCourseCodeAsync(cc);
                return Ok(batches);
            }
            catch (BatchException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Course")]
        public async Task<ActionResult> InsertCourse(Course course)
        {
            try
            {
                await batchRepo.InsertCourseAsync(course);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

