
using CarQuotationSystem.Data;
using CarQuotationSystem.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuotationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotations()
        {
            var quotations = await _context.Quotations
                .Include(q => q.Dealer)
                .Include(q => q.PurchaseRequest)
                .ToListAsync();

            return Ok(quotations);
        }

        [HttpGet("purchase-request/{id}")]
        public async Task<IActionResult> GetQuotationsByPurchaseRequest(int id)
        {
            var quotations = await _context.Quotations
                .Include(q => q.Dealer)
                .Include(q => q.PurchaseRequest)
                .Where(q => q.PurchaseRequestId == id)
                .ToListAsync();

            return Ok(quotations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotation(int id)
        {
            var quotation = await _context.Quotations
                .Include(q => q.Dealer)
                .Include(q => q.PurchaseRequest)
                .FirstOrDefaultAsync(q => q.QuotationId == id);

            if (quotation == null)
            {
                return NotFound("Quotation not found.");
            }

            return Ok(quotation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuotation(Quotation quotation)
        {
            var purchaseRequest = await _context.PurchaseRequests
                .FindAsync(quotation.PurchaseRequestId);

            if (purchaseRequest == null)
            {
                return NotFound("Purchase request not found.");
            }

            var dealer = await _context.Dealers
                .FindAsync(quotation.DealerId);

            if (dealer == null)
            {
                return NotFound("Dealer not found.");
            }

            quotation.QuotationDate = DateTime.Now;
            quotation.ValidUntil = DateTime.Now.AddDays(30);
            quotation.Status = "Pending";

            quotation.FinalPrice =
                quotation.CarPrice +
                quotation.AccessoryPrice +
                quotation.Insurance +
                quotation.Tax -
                quotation.Discount;

            _context.Quotations.Add(quotation);

            await _context.SaveChangesAsync();

            return Ok(quotation);
        }
    }
}
