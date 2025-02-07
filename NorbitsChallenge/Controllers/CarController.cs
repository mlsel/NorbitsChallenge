using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class CarController : Controller
    {
        private readonly CarDb _carDb;

        public CarController(IConfiguration config)
        {
            _carDb = new CarDb(config);
        }

        public IActionResult Index()
        {
            int companyId = UserHelper.GetLoggedOnUserCompanyId();
            List<Car> cars = _carDb.GetAllCars(companyId);
            return View(cars);
        }

        [HttpGet("Car/Create")]
        public IActionResult Create()
        {
            var model = new Car
            {
                CompanyId = UserHelper.GetLoggedOnUserCompanyId() // Henter CompanyId()
            };
            return View(model);
        }

        [HttpPost("Car/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _carDb.AddCar(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [HttpGet("Car/{licensePlate}")]
        public IActionResult ViewCar(string licensePlate)
        {
            int companyId = UserHelper.GetLoggedOnUserCompanyId();
            var car = _carDb.GetCarDetails(companyId, licensePlate);

            if (car == null)
            {
                return NotFound("Bil ikkje funnen i databasen.");
            }

            return View(car);
        }

        [HttpGet("Car/Edit/{licensePlate}")]
        public IActionResult Edit(string licensePlate)
        {
            int companyId = UserHelper.GetLoggedOnUserCompanyId();
            var car = _carDb.GetCarDetails(companyId, licensePlate);

            if (car == null)
            {
                return NotFound("Bilen ble ikke funnet.");
            }

            return View(car);
        }

        [HttpPost("Car/Edit/{licensePlate}")]
        public IActionResult Edit(Car car)
        {
            int companyId = UserHelper.GetLoggedOnUserCompanyId();
            car.CompanyId = companyId;

            _carDb.UpdateCar(car);
            return RedirectToAction("ViewCar", new { licensePlate = car.LicensePlate });
        }

        [HttpPost("Car/Delete/{licensePlate}")]
        public IActionResult Delete(string licensePlate)
        {
            int companyId = UserHelper.GetLoggedOnUserCompanyId();
            bool deleted = _carDb.DeleteCar(companyId, licensePlate); // Implement in `CarDb.cs`

            if (!deleted)
            {
                return NotFound("Bilen ble ikke funnet eller kunne ikke slettes.");
            }

            return RedirectToAction("Index");
        }
    }
}

