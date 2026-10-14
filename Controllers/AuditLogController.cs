using CarQuotationSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarQuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuditLogController(
            CarQuotationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _context.AuditLogs
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(logs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog(
            int? userId,
            string action,
            string description)
        {
            var log = new CarQuotationSystem.models.AuditLog
            {
                UserId = userId,
                Action = action,
                Description = description,
                CreatedAt = DateTime.Now
            };

            _context.AuditLogs.Add(log);

            await _context.SaveChangesAsync();

            return Ok("Audit log created successfully.");
        }
    }
}