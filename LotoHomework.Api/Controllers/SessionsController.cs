using LotoHomework.Domain.Statics;
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

        [HttpGet("start")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> StartSession()
        {
            try
            {
                if(await _sessionService.StartSession())
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
    }
}
