using LotoHomework.Domain.Models;
using LotoHomework.DTOs.SessionDTOs;

namespace LotoHomework.Services.Interfaces
{
    public interface ISessionService
    {
        Task<bool> StartSession();
        Task<Session> GetLatest();
        Task StartDraw(int adminId);
        Task<SessionWinnersDto> GetWinners();
    }
}
