using CreditGrid.Notifier.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreditGrid.Notifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly IReminderService reminderService;

        public NotifyController(IReminderService reminderService)
        {
            this.reminderService = reminderService;
        }

        [HttpGet("{creditNumber}")]
        public async Task<IActionResult> Reminder(string creditNumber)
        {

            await reminderService.RaiseReminderAsync(creditNumber);

            return Ok(creditNumber);
        }
    }
}
