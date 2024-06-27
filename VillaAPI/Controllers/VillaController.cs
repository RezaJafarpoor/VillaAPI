using AutoMapper;
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
    private readonly IMapper _mapper;

    public VillaController( ILogger<VillaController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
    {
        IEnumerable<Villa> villaList =await _dbContext.Villas.ToListAsync();
        
        _logger.LogInformation("Getting all villas");
        return Ok(_mapper.Map<List<VillaDto>>(villaList));
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

        
        return Ok(_mapper.Map<Villa>(villa));
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
    {

        if (_dbContext.Villas.FirstOrDefault(x => x.Name.ToLower() == villaCreateDto.Name.ToLower())!= null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");  
        }
        if (villaCreateDto is null)
        {
            return BadRequest("villa");
        }

        var model = _mapper.Map <Villa>(villaCreateDto);
       
    
       await _dbContext.Villas.AddAsync(model);
        await _dbContext.SaveChangesAsync();
       
        return CreatedAtAction(nameof(GetVilla), new {id = model.Id}, model);
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
    public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
    {
        if (villaUpdateDto is null || id != villaUpdateDto.Id)
        {
            return BadRequest(villaUpdateDto);
        }

        var villa = _mapper.Map<Villa>(villaUpdateDto);
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

        var villaDto = _mapper.Map<VillaUpdateDto>(villa); 
        patch.ApplyTo(villaDto,ModelState);
        var model = _mapper.Map<Villa>(villa);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
       
        _dbContext.Villas.Update(model);
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}