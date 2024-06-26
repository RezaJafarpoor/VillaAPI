using Microsoft.AspNetCore.Mvc;
using VillaAPI.Entities;

namespace VillaAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VillaController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa>
        {
            new Villa { Id = 1, Name = "Pool View" },
            new Villa { Id = 2, Name = "Beach View" }

        };
    }
    
}