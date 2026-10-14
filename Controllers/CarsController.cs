using CarQuotationSystem.Data;
using CarQuotationSystem.models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound("Car not found.");
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            _context.Cars.Add(car);

            await _context.SaveChangesAsync();

            return Ok("Car created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(
            int id,
            Car car)
        {
            var existingCar = await _context.Cars.FindAsync(id);

            if (existingCar == null)
            {
                return NotFound("Car not found.");
            }

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.Variant = car.Variant;
            existingCar.BasePrice = car.BasePrice;

            await _context.SaveChangesAsync();

            return Ok("Car updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound("Car not found.");
            }

            _context.Cars.Remove(car);

            await _context.SaveChangesAsync();

            return Ok("Car deleted successfully.");
        }
    }
}