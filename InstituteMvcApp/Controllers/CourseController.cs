using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InstituteMvcApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace InstituteMvcApp.Controllers
{
    [Authorize]

    public class CourseController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5070/api/Course/") };
        static string token;
        public async Task<ActionResult> Index()
        {
            token = HttpContext.Session.GetString("token");
            string role = User.Claims.ToArray()[4].Value;
            ViewBag.Role = role;    //Uses dynamic properties

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Course> courses = await client.GetFromJsonAsync<List<Course>>("");
            return View(courses);
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(string cc)
        {
            Course course = await client.GetFromJsonAsync<Course>("" + cc);
            return View(course);
        }

        // GET: CourseController/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Course course)
        {
            try
            {
                await client.PostAsJsonAsync<Course>(""+token,course);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        [Route("Course/Edit/{cc}")]
        public async Task<ActionResult> Edit(string cc)
        {
            Course course = await client.GetFromJsonAsync<Course>("" + cc);
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Course/Edit/{cc}")]
        public async Task<ActionResult> Edit(string cc, Course course)
        {
            try
            {
                await client.PutAsJsonAsync("" + cc, course);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        [Route("Course/Delete/{cc}")]
        public async Task<ActionResult> Delete(string cc)
        {
            Course course = await client.GetFromJsonAsync<Course>("" + cc);
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Course/Delete/{cc}")]
        public async Task<ActionResult> Delete(string cc, IFormCollection collection)
        {
            try
            {
                /*
                HttpResponseMessage message = await client.DeleteAsync("" + token + "/" + cc);
                if (message.IsSuccessStatusCode) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string errMsg = await message.Content.ReadAsStringAsync();
                    ViewBag.Error = errMsg;
                    throw new Exception();
                }*/
                await client.DeleteAsync("" + cc);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
