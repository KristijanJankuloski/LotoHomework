using LotoHomework.Domain.Models;
using LotoHomework.DTOs.CombinationDTOs;
using LotoHomework.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotoHomework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidsController : ControllerBase
    {
        private readonly ICombinationService _combinationService;
        private readonly ISessionService _sessionService;
        public BidsController(ICombinationService combinationService, ISessionService sessionService)
        {
            _combinationService = combinationService;
            _sessionService = sessionService;
        }

        [HttpPost]
        public async Task<ActionResult<BidCreateResponseDto>> Create([FromBody] CombinationCreateDto dto)
        {
            try
            {
                int userId = int.Parse(HttpContext.User.FindFirst("id")?.Value);
                Session session = await _sessionService.GetLatest();
                if (session == null)
                {
                    return BadRequest("Not session in progress");
                }
                await _combinationService.Create(dto, userId, session.Id);
                return StatusCode(StatusCodes.Status201Created, new BidCreateResponseDto { Message = "Bid has been created, wait for a draw and go to the link", Link = "sessions/winners" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
