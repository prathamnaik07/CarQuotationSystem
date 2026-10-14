using CarQuotationSystem.Data;
using CarQuotationSystem.dtos;
using CarQuotationSystem.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseRequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRequestController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePurchaseRequest(PurchaseRequest request)
        {
            var car = await _context.Cars.FindAsync(request.CarId);

            if (car == null)
            {
                return NotFound("Car not found.");
            }

            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            request.RequestDate = DateTime.Now;
            request.Status = "Requested";

            _context.PurchaseRequests.Add(request);
            await _context.SaveChangesAsync();

            var dealers = await _context.Dealers.ToListAsync();

            if (dealers.Count == 0)
            {
                return Ok(new
                {
                    message = "Purchase request created, but no dealers are available.",
                    purchaseRequestId = request.PurchaseRequestId
                });
            }

            foreach (var dealer in dealers)
            {
                var quotation = new Quotation
                {
                    PurchaseRequestId = request.PurchaseRequestId,
                    DealerId = dealer.DealerId,
                    CarPrice = car.BasePrice,
                    AccessoryPrice = request.TotalAccessoryPrice,
                    Insurance = 10000,
                    Tax = car.BasePrice * 0.05m,
                    Discount = car.BasePrice * 0.02m,
                    QuotationDate = DateTime.Now,
                    ValidUntil = DateTime.Now.AddDays(30),
                    Status = "Pending"
                };

                quotation.FinalPrice =
                    quotation.CarPrice +
                    quotation.AccessoryPrice +
                    quotation.Insurance +
                    quotation.Tax -
                    quotation.Discount;

                _context.Quotations.Add(quotation);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Purchase request and quotations created successfully.",
                purchaseRequestId = request.PurchaseRequestId
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetPurchaseRequests()
        {
            var requests = await _context.PurchaseRequests
                .Include(x => x.Car)
                .Select(x => new
                {
                    x.PurchaseRequestId,
                    x.UserId,
                    x.CarId,
                    CarName = x.Car.Brand + " " + x.Car.Model,
                    x.WantsAccessories,
                    x.TotalAccessoryPrice,
                    x.RequestDate,
                    x.Status
                })
                .ToListAsync();

            return Ok(requests);
        }
    }
}