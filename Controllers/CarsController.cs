using CarDealerWebApp.Data;
using CarDealerWebApp.Models;
using CarDealerWebApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace CarDealerWebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public CarsController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {           
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(IFormFile image, [Bind("CarId,Make,Model,Year,Price,Description,ImageUrl,ImagePath")] Cars cars)
        {
            if (image == null || string.IsNullOrEmpty(image.FileName) || image.Length <= 0)
            {
                ModelState.AddModelError("Image", "Please upload a valid image file.");
                return View();
            }

            string imagedir = Path.Combine("wwwroot", "Images");
            if (!Path.Exists(imagedir))
            {
                Directory.CreateDirectory(imagedir);
            }
            string ImagePath = Path.Combine(imagedir, image.FileName);

            using (var fileStream = new FileStream(Path.Combine(imagedir, image.FileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }


            cars.ImageUrl = ImagePath;

            //saving to the database
            _context.Add(cars);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cars");
        }

        public IActionResult Inquiry(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            // Create a view model to pass to the view (if needed for form data)
            var inquiryModel = new Inquiry
            {
                CarId = car.CarId,
            };

            return View(inquiryModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendInquiry(Inquiry model)
        {
            var UserEmail = "neetagaikwad072@gmail.com";//model.UserEmail; 
            var AdminEmail = "neetagaikwad072@gmail.com";
            var car = await _context.Cars.FindAsync(model.CarId);
            if (car == null)
            {
                return NotFound();
            }
            // Save the inquiry in the database
            var inquiry = new Inquiry
            {
                CarId = model.CarId,
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                Message = model.Message,
               
            };

            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();
            var subject = $"Inquiry for {car.Make} {car.Model} {car.Year}";

            var body = $"User: {model.UserName}\nEmail: {model.UserEmail}\nMessage: {model.Message}\nCar Details:\n{car.Make} {car.Model} ({car.Year}) - £{car.Price}";            

            // send email to admin
            await _emailService.SendEmailAsync(AdminEmail, subject, body);
           
            // Send email to user
            var userSubject = "Thank you for your inquiry!";
            var userBody = $"Dear {model.UserName},\n\nThank you for your inquiry regarding the {car.Make} {car.Model}. We will get back to you shortly.\n\nBest regards,\nCar Dealership Team";
            await _emailService.SendEmailAsync(UserEmail, userSubject, userBody);

            // Redirect after sending the email
            return RedirectToAction("Index", "Cars");          
        }

    }
}
