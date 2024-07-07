using InstituteMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InstituteMvcApp.Controllers
{
    [Authorize]
    public class BatchController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5263/api/Batch/") };
        static string token;
        public async Task<ActionResult> Index()
        {
            token = HttpContext.Session.GetString("token");
            string role = User.Claims.ToArray()[4].Value;
            ViewBag.Role = role;    //Uses dynamic properties

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Batch> batches = await client.GetFromJsonAsync<List<Batch>>("");
            return View(batches);
        }

        // GET: BatchController/Details/5
        public async Task<ActionResult> Details(string bc)
        {
            Batch batch = await client.GetFromJsonAsync<Batch>(""+bc);
            return View(batch);
        }

        // GET: BatchController/Create
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: BatchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Batch batch)
        {
            try
            {
                await client.PostAsJsonAsync<Batch>("" + token, batch);
                return RedirectToAction(nameof(Index));
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //await client.PostAsJsonAsync("", batch);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BatchController/Edit/5
        [Route("Batch/Edit/{bc}")]
        public async Task<ActionResult> Edit(string bc)
        {
            Batch batch = await client.GetFromJsonAsync<Batch>("" + bc);
            return View(batch);
        }

        // POST: BatchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Batch/Edit/{bc}")]
        public async Task<ActionResult> Edit(string bc, Batch batch)
        {
            try
            {
                await client.PutAsJsonAsync("" + bc, batch);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BatchController/Delete/5
        [Route("Batch/Delete/{bc}")]
        public async Task<ActionResult> Delete(string bc)
        {
            Batch batch = await client.GetFromJsonAsync<Batch>("" + bc);
            return View(batch);
        }

        // POST: BatchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Batch/Delete/{bc}")]
        public async Task<ActionResult> Delete(string bc, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + bc);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByCourseCode(string cc)
        {
            List<Batch> batches = await client.GetFromJsonAsync<List<Batch>>("ByCourseCode/" + cc);
            return View(batches);
        }
    }
}
