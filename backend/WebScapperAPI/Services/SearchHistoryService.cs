using Microsoft.EntityFrameworkCore;
using WebScapperAPI.Data;
using WebScapperAPI.Modules;

namespace WebScapperAPI.Services
{
    public class SearchHistoryService
    {
        private readonly EngineResultDbContext _dbContext;

        public SearchHistoryService(EngineResultDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EngineResultDto> GetAllSearchHistory()
        {
            var searchHistory = _dbContext.EngineResults.ToList();

            var resultDtoList = searchHistory.Select(result => new EngineResultDto
            {
                KeyWords = result.KeyWords,
                Url = result.Url,
                EngineName = result.EngineName,
                Ranking = result.Ranking,
                TimeStamp = result.TimeStamp
            }).ToList();

            return resultDtoList;
        }

        public void DeleteAllSearchHistory()
        {
            var searchHistory = _dbContext.EngineResults.ToList();
            _dbContext.EngineResults.RemoveRange(searchHistory);
            _dbContext.SaveChanges();
        }
    }
}
