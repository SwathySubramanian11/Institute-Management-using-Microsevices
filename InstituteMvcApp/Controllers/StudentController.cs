using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InstituteMvcApp.Models;

namespace InstituteMvcApp.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5030/api/Student/") };
        static string token;
        public async Task<ActionResult> Index()
        {
            token = HttpContext.Session.GetString("token");
            string role = User.Claims.ToArray()[4].Value;
            ViewBag.Role = role;    //Uses dynamic properties

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Student> students = await client.GetFromJsonAsync<List<Student>>("");
            return View(students);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(string rno)
        {
            Student student=await client.GetFromJsonAsync<Student>(""+rno);
            return View(student);
        }

        // GET: StudentController/Create
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                await client.PostAsJsonAsync<Student>("", student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        [Route("Student/Edit/{rno}")]
        public async Task<ActionResult> Edit(string rno)
        {
            Student student = await client.GetFromJsonAsync<Student>(""+rno);
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Student/Edit/{rno}")]
        public async Task<ActionResult> Edit(string rno, Student student)
        {
            try
            {
                await client.PutAsJsonAsync("" + rno, student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        [Route("Student/Delete/{rno}")]
        public async Task<ActionResult> Delete(string rno)
        {
            Student student = await client.GetFromJsonAsync<Student>("" + rno);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Student/Delete/{rno}")]
        public async Task<ActionResult> Delete(string rno, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + rno);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult>ByBatchCode(string bc)
        {
            List<Student> students=await client.GetFromJsonAsync<List<Student>>("ByBatchCode/"+bc);
            return View(students);
        }
    }
}
