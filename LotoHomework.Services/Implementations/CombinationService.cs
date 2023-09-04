using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Domain.Models;
using LotoHomework.DTOs.CombinationDTOs;
using LotoHomework.Services.Interfaces;

namespace LotoHomework.Services.Implementations
{
    public class CombinationService : ICombinationService
    {
        private readonly ICombinationRepository _combinationRepository;
        public CombinationService(ICombinationRepository combinationRepository)
        {
            _combinationRepository = combinationRepository; 
        }

        public async Task Create(CombinationCreateDto dto, int userId, int sessionId)
        {
            Combination combination = new Combination()
            {
                UserId = userId,
                SessionId = sessionId,
                EntryTime = DateTime.Now,
            };
            combination.SetNumbers(dto.Numbers.ToList());
            await _combinationRepository.InsertAsync(combination);
        }
    }
}
