using Microsoft.AspNetCore.Mvc;

namespace Partia.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PolitycyController : ControllerBase
{
    private readonly IDbService _dbService;

    public PolitycyController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPolitycy()
    {
        var politycy = await _dbService.GetPolitycy();
        return Ok(politycy);
    }
    
}