using InstituteMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace InstituteMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                var claims = User.Claims.ToArray();

                if (claims.Length > 4)
                {
                    // Get the role from the claims array
                    string role = claims[4].Value;

                    // Prepare to obtain a token from the authentication service
                    string secretKey = "My name is James Bond 007 the great";
                    using (HttpClient authClient = new HttpClient { BaseAddress = new Uri("http://localhost:5087/api/Auth/") })
                    {
                        // Call the authentication service to get a token
                        string token = await authClient.GetStringAsync($"Token/{username}/{role}/{secretKey}");

                        // Store the token in Session object
                        HttpContext.Session.SetString("token", token);
                    }
                }
                else
                {
                    // Handle the case where there are not enough claims
                    _logger.LogError("Insufficient claims for user: " + username);
                    return RedirectToAction("Error", "Home"); // Redirect to error page or handle differently
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
