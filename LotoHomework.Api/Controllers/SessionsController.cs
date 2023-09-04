using LotoHomework.Domain.Statics;
using LotoHomework.DTOs.SessionDTOs;
using LotoHomework.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotoHomework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("winners")]
        public async Task<ActionResult<SessionWinnersDto>> GetWinners()
        {
            try
            {
                var winners = await _sessionService.GetWinners();
                if (winners == null)
                {
                    return NotFound("No prevoius session");
                }
                return Ok(winners);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("start")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> StartSession()
        {
            try
            {
                if (await _sessionService.StartSession())
                {
                    return StatusCode(StatusCodes.Status201Created, "Session started");
                }
                return StatusCode(StatusCodes.Status405MethodNotAllowed, "Session is already in progress");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("draw")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> StartDraw()
        {
            try
            {
                int adminId = int.Parse(HttpContext.User.FindFirst("id")?.Value);
                await _sessionService.StartDraw(adminId);
                if (await _sessionService.StartSession())
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
