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

                //Sending request to find web api REST service resource GetCompanyParkingById using HttpClient
                int id = 5;
                HttpResponseMessage result = await client.GetAsync($"Parking/GetCompanyParkingById/{id}");

                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    parking = result.Content.ReadAsAsync<ParkingDto>().Result;
                    
                }
            }
            //var ParkingList = new List<ParkingDto>() { new ParkingDto
            //{
            //    Id = parking.Id,
            //    Name = parking.Name,
            //    ParkingList = parking.ParkingList.ToList()
            //} }.ToList();

            //pass parking list to the razor page
            ViewBag.Parking = parking;

            return View();
        }

        public async Task<IActionResult> CarFinder(string CarNumber)
        {
            string SpaceNumber = "";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                //Sending request to find web api REST service resource CarFinder using HttpClient
                CarNumber = "909UAL";
                HttpResponseMessage result = await client.GetAsync($"Spaces/CarFinder/{CarNumber}");

                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    SpaceNumber = result.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    SpaceNumber = "car number underfiend!, please check if you write it correct.";
                }
            }
            //return parking list
            return View(SpaceNumber);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
