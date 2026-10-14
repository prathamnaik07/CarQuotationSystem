using CarQuotationSystem.Data;
using CarQuotationSystem.models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DealersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDealers()
        {
            var dealers = await _context.Dealers.ToListAsync();

            return Ok(dealers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealer(int id)
        {
            var dealer = await _context.Dealers.FindAsync(id);

            if (dealer == null)
            {
                return NotFound("Dealer not found.");
            }

            return Ok(dealer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDealer(
            Dealer dealer)
        {
            _context.Dealers.Add(dealer);

            await _context.SaveChangesAsync();

            return Ok("Dealer created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDealer(
            int id,
            Dealer dealer)
        {
            var existingDealer =
                await _context.Dealers.FindAsync(id);

            if (existingDealer == null)
            {
                return NotFound("Dealer not found.");
            }

            existingDealer.DealerName = dealer.DealerName;
            existingDealer.Email = dealer.Email;
            existingDealer.PhoneNumber = dealer.PhoneNumber;
            existingDealer.address = dealer.address;

            await _context.SaveChangesAsync();

            return Ok("Dealer updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDealer(int id)
        {
            var dealer = await _context.Dealers.FindAsync(id);

            if (dealer == null)
            {
                return NotFound("Dealer not found.");
            }

            _context.Dealers.Remove(dealer);

            await _context.SaveChangesAsync();

            return Ok("Dealer deleted successfully.");
        }
    }
}