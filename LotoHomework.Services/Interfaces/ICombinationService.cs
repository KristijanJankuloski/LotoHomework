using LotoHomework.DTOs.CombinationDTOs;

namespace LotoHomework.Services.Interfaces
{
    public interface ICombinationService
    {
        Task Create(CombinationCreateDto dto, int userId, int sessionId);
    }
}
