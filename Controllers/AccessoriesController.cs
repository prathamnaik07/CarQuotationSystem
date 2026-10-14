using CarQuotationSystem.Data;
using CarQuotationSystem.models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccessoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccessories()
        {
            var accessories =
                await _context.Accessories.ToListAsync();

            return Ok(accessories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessory(int id)
        {
            var accessory =
                await _context.Accessories.FindAsync(id);

            if (accessory == null)
            {
                return NotFound("Accessory not found.");
            }

            return Ok(accessory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccessory(
            Accessory accessory)
        {
            _context.Accessories.Add(accessory);

            await _context.SaveChangesAsync();

            return Ok("Accessory created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccessory(
            int id,
            Accessory accessory)
        {
            var existingAccessory =
                await _context.Accessories.FindAsync(id);

            if (existingAccessory == null)
            {
                return NotFound("Accessory not found.");
            }

            existingAccessory.Name = accessory.Name;
            existingAccessory.Price = accessory.Price;

            await _context.SaveChangesAsync();

            return Ok("Accessory updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessory(int id)
        {
            var accessory =
                await _context.Accessories.FindAsync(id);

            if (accessory == null)
            {
                return NotFound("Accessory not found.");
            }

            _context.Accessories.Remove(accessory);

            await _context.SaveChangesAsync();

            return Ok("Accessory deleted successfully.");
        }
    }
}