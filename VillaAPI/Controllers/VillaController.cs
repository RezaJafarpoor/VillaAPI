using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
    {
        var villas =await _dbContext.Villas.ToListAsync();
        _logger.LogInformation("Getting all villas");
        return Ok(villas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VillaDto>> GetVilla(int id)
    {
        var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
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

    public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villaDto)
    {

        if (_dbContext.Villas.FirstOrDefault(x => x.Name.ToLower() == villaDto.Name.ToLower())!= null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");  
        }
        if (villaDto is null)
        {
            return BadRequest("villa");
        }
        

        var villa = new Villa()
        {
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

       await _dbContext.Villas.AddAsync(villa);
        await _dbContext.SaveChangesAsync();
       
        return CreatedAtAction(nameof(GetVilla), new {id = villa.Id}, villa);
    }

    [HttpDelete($"{{id:int}}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        if (id ==0)
        {
            return BadRequest();
        }

        var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _dbContext.Villas.Remove(villa);
        await _dbContext.SaveChangesAsync();

       
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
    {
        if (villaDto is null || id != villaDto.Id)
        {
            return BadRequest(villaDto);
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
        await _dbContext.SaveChangesAsync();
        
        return NoContent();

    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patch)
    {
        if (patch == null || id == 0)
        {
            return BadRequest();
        }
        

        var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        var villaDto = new VillaUpdateDto()
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
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}