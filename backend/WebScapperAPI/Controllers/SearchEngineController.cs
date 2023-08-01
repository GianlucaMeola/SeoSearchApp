using Microsoft.AspNetCore.Mvc;
using WebScapperAPI.Modules;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly SearchEngineService _searchEngineService;

    public SearchController(SearchEngineService searchEngineService)
    {
        _searchEngineService = searchEngineService;
    }

    [HttpGet("{keyword}/{url}/{engineNames}")]
    public ActionResult<EngineResultDto> EngineSearch(string keyword, string url, string engineNames)
    {
        if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(url))
        {
            return BadRequest("Keyword and URL are required.");
        }

        var results = _searchEngineService.PerformEngineSearch(keyword, url, engineNames);
        return Ok(results);
    }
}
