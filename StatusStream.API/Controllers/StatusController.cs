 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusStream.Application.DTOs;
using StatusStream.Application.Services;
using StatusStream.Domain.Entities;

namespace StatusStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        [HttpPost("PostStatus")]
        public IActionResult PostStatus([FromBody] StatusDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.User) || string.IsNullOrWhiteSpace(dto.Message))
            {
                return BadRequest("User and message are required.");
            }
            var status = new Status(0, dto.User, dto.Message);
            var addedStatus = _statusService.Add(status);
            var responseDto = new StatusResponseDto
            {
                Id = addedStatus.Id,
                User = addedStatus.User,
                Message = addedStatus.Message,
                Timestamp= addedStatus.Timestamp
            };
            return CreatedAtAction(nameof(GetAllStatuses), new { id = addedStatus.Id }, responseDto);

        }
        [HttpGet("GetAllStatuses")]
        public IActionResult GetAllStatuses([FromQuery] string? user = null)
        {
            var statuses = _statusService.GetAll(user);
            var responseDtos = statuses.Select(s => new StatusResponseDto
            {
                Id = s.Id,
                User = s.User,
                Message = s.Message,
                Timestamp = s.Timestamp
            });
            return Ok(responseDtos);
        }
    }
}
