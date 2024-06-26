﻿using Microsoft.AspNetCore.Mvc;
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
        return Ok();
    }
}