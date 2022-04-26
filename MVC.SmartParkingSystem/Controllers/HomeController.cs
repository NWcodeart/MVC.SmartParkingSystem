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
        private string SN = "";
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

        //show all vacant spaces page 
        public async Task<IActionResult> ParkingFinder()
        {
            //Parking space number - recived from carFinder
            ViewBag.SpaceParking = SN;

            //parking model
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
            //pass parking spaces list to the view
            List<SpacesDto> spaces = parking.ParkingList.ToList();
            return View(spaces);
        }

        //call request of the users' car place 
        [HttpPost]
        public async Task<ActionResult> CarFinder(string CarNumber)
        {
            string SpaceNumber = "";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                //Sending request to find web api REST service resource CarFinder using HttpClient
                int id = 5;
                HttpResponseMessage result = await client.GetAsync($"Spaces/CarFinder/{CarNumber}/{id}");

                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    SpaceNumber = result.Content.ReadAsStringAsync().Result;

                    //carry string with viewBag
                    ViewBag.Massege = "You are parked your car at:";
                    ViewBag.style = "";
                    ViewBag.SpaceNumber = SpaceNumber;
                }
                else
                {
                    //bad request
                    ViewBag.Massege = "car number underfiend!, please check if you write it correct.";
                    ViewBag.style = "red-font";
                    ViewBag.SpaceNumber = "Notes: try again without; spaces, arabic letters, or other non english characters";
                }

                return PartialView("carFinder");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
