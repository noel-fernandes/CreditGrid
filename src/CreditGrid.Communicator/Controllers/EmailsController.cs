using CreditGrid.Communicator.Controllers.Models;
using CreditGrid.Communicator.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CreditGrid.Communicator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailsController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> SendEmail([FromBody] EmailMessageDto emailMessage)
        {
            var statusCode = await this.emailService.SendEmailAsync(emailMessage.Recipient.Name, emailMessage.Recipient.Email, emailMessage.Subject, emailMessage.MessageBody);
            return Ok(statusCode);
        }
    }
}
