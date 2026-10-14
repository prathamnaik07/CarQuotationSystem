using CarQuotationSystem.Data;
using CarQuotationSystem.models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CompletePurchase(
            int purchaseRequestId,
            int quotationId,
            int userId)
        {
            var quotation =
                await _context.Quotations
                .FindAsync(quotationId);

            if (quotation == null)
            {
                return NotFound("Quotation not found.");
            }

            var purchaseRequest =
                await _context.PurchaseRequests
                .FindAsync(purchaseRequestId);

            if (purchaseRequest == null)
            {
                return NotFound("Purchase request not found.");
            }

            var purchase = new Purchase
            {
                PurchaseRequestId = purchaseRequestId,
                QuotationId = quotationId,
                UserId = userId,
                PurchaseDate = DateTime.Now,
                FinalAmount = quotation.FinalPrice,
                Status = "Completed"
            };

            _context.Purchases.Add(purchase);

            purchaseRequest.Status = "Purchased";

            quotation.Status = "Selected";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Purchase completed successfully.",
                purchaseId = purchase.PurchaseId,
                finalAmount = purchase.FinalAmount
            });
        }
    }
}