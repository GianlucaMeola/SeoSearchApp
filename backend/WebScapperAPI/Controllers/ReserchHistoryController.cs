using Microsoft.AspNetCore.Mvc;
using WebScapperAPI.Modules;
using WebScapperAPI.Services;

namespace WebScapperAPI.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class ReserchHistoryController : Controller
    {
        private readonly SearchHistoryService _searchHistoryService;

        public ReserchHistoryController(SearchHistoryService searchHistoryService)
        {
            _searchHistoryService = searchHistoryService;
        }
        [HttpGet]
        public ActionResult<EngineResultDto> GetAll()
        {
            List<EngineResultDto> result = _searchHistoryService.GetAllSearchHistory();
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _searchHistoryService.DeleteAllSearchHistory();
            return NoContent();
        }
    }
}
