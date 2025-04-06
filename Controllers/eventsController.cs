using Azure.Messaging.ServiceBus;
using EventSubAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EventSubAPI.Services;
namespace EventSubAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]/")]
    public class eventsController : ControllerBase
    {
        private readonly ServiceBusClientClass _serviceBusClientClass;

        public eventsController(ServiceBusClientClass serviceBusClientClass)
        {
            _serviceBusClientClass = serviceBusClientClass;
        }

        [HttpPost("receive")]
        public async Task<IActionResult> ReceiveEvent([FromBody] EventModel eventModel)
        {
            if (eventModel == null)
            {
                return BadRequest("Null event.");
            }
            var eventJson = System.Text.Json.JsonSerializer.Serialize(eventModel);
            await _serviceBusClientClass.SendMessageAsync(eventJson);

            return Ok("Event message successfully forwarded to the Azure Service Bus.");
        }

    }
}
