using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Dtos;
using VillaAPI.Entities;

namespace VillaAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VillaController : ControllerBase
{ 
    [HttpGet]
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        if (id ==0)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        if (villa is null)
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

    public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villa)
    {
        if (villa is null)
        {
            return BadRequest("villa");
        }

        if (villa.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villa.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villa);
        return CreatedAtAction(nameof(GetVilla), new {id = villa.Id}, villa);
    }
}