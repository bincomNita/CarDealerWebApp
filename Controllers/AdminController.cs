using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "password123")
            {
                TempData["Message"] = "Login successful!";
                //AddCar
                return RedirectToAction("AddCar", "Cars");
                //return RedirectToAction("Index", "Cars");
            }

            ViewBag.ErrorMessage = "Invalid username or password!";
            return View();
        }
    }
}
