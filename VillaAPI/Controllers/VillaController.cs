using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.DTOs;
using VillaAPI.Entities;

namespace VillaAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VillaController : ControllerBase
{
    private readonly ILogger<VillaController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public VillaController( ILogger<VillaController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    [HttpGet]
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        var villas = _dbContext.Villas.ToList();
        _logger.LogInformation("Getting all villas");
        return Ok(villas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
        _logger.LogInformation("Getting one villa");

        if (id ==0)
        {
            return BadRequest();
        }

        if (villa == null)
        {
            return NotFound();
        }

        
        return Ok(villa);
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
    {

        if (_dbContext.Villas.FirstOrDefault(x => x.Name.ToLower() == villaDto.Name.ToLower())!= null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");  
        }
        if (villaDto is null)
        {
            return BadRequest("villa");
        }
        if (villaDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var villa = new Villa
        {
            Id = villaDto.Id,
            Name = villaDto.Name,
            Amenity = villaDto.Amenity,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Occupancy = villaDto.Occupancy,
            ImageUrl = villaDto.ImageUrl,
            Rate = villaDto.Rate,
            Sqft = villaDto.Sqft,
            Details = villaDto.Details

        };

        _dbContext.Villas.Add(villa);
        _dbContext.SaveChanges();
       
        return CreatedAtAction(nameof(GetVilla), new {id = villa.Id}, villa);
    }

    [HttpDelete($"{{id:int}}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteVilla(int id)
    {
        if (id ==0)
        {
            return BadRequest();
        }

        var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _dbContext.Villas.Remove(villa);
        _dbContext.SaveChanges();

       
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
    {
        if (villaDto is null || id != villaDto.Id)
        {
            return BadRequest();
        }
        var villa = new Villa
        {
            Id = villaDto.Id,
            Name = villaDto.Name,
            Amenity = villaDto.Amenity,
            UpdatedDate = DateTime.UtcNow,
            Occupancy = villaDto.Occupancy,
            ImageUrl = villaDto.ImageUrl,
            Rate = villaDto.Rate,
            Sqft = villaDto.Sqft,
            Details = villaDto.Details

        };
        _dbContext.Villas.Update(villa);
        _dbContext.SaveChanges();
        
        return NoContent();

    }

    [HttpPatch]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patch)
    {
        if (patch == null || id == 0)
        {
            return BadRequest();
        }
        

        var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        var villaDto = new VillaDto
        {
            Id = villa.Id,
            Name = villa.Name,
            Amenity = villa.Amenity,
            Occupancy = villa.Occupancy,
            ImageUrl = villa.ImageUrl,
            Rate = villa.Rate,
            Sqft = villa.Sqft,
            Details = villa.Details

        };
        patch.ApplyTo(villaDto,ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var updatedVilla = new Villa
        {
            Id = villaDto.Id,
            Name = villaDto.Name,
            Amenity = villaDto.Amenity,
            UpdatedDate = DateTime.UtcNow,
            Occupancy = villaDto.Occupancy,
            ImageUrl = villaDto.ImageUrl,
            Rate = villaDto.Rate,
            Sqft = villaDto.Sqft,
            Details = villaDto.Details

        };
        _dbContext.Villas.Update(updatedVilla);
        _dbContext.SaveChanges();
        
        return NoContent();
    }
}