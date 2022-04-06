using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.SmartParkingSystem.Models;
using SmartParkingSystem.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.SmartParkingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44312/api/";

        //home page 
        public IActionResult Index()
        {
            return View();
        }

        //all vacant spaces page 
        public async Task<IActionResult> ParkingFinder()
        {
            var parking = new ParkingDto();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                //It clears the default headers that are sent with every request.
                //These headers are things that are common to all your requests, e.g. Content-Type, Authorization, etc.
                //client.DefaultRequestHeaders.Clear();


                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                int id = 3;
                HttpResponseMessage result = await client.GetAsync($"Parking/GetCompanyParkingById/{id}");

                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    parking = result.Content.ReadAsAsync<ParkingDto>().Result;
                    
                }
            }
            //return parking list
            return View(parking);
        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
