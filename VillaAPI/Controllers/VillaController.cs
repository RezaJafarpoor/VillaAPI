using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.DTOs;
using VillaAPI.Entities;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaController : ControllerBase
{
    private ApiResponse _response = new ApiResponse();
    private readonly ILogger<VillaController> _logger;
    private readonly IVillaRepository _dbVilla;
    private readonly IMapper _mapper;

    public VillaController(ILogger<VillaController> logger, IVillaRepository dbVilla, IMapper mapper)
    {
        _logger = logger;
        _dbVilla = dbVilla;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetVillas()
    {
        try
        {
            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();

            _logger.LogInformation("Getting all villas");
            _response.Result = _mapper.Map<List<VillaDto>>(villaList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErroMessages = [e.ToString()];
        }
        return _response;


    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> GetVilla(int id)
    {
        try
        {
            var villa = await _dbVilla.GetAsync(x => x.Id == id);
            _logger.LogInformation("Getting one villa");

            if (id == 0) return BadRequest();

            if (villa == null) return NotFound();
            _response.Result = _mapper.Map<Villa>(villa);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErroMessages = [e.ToString()];
        }

        return _response;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
    {
        try
        {
            if (_dbVilla.GetAsync(x => x.Name.ToLower() == villaCreateDto.Name.ToLower()) != null)
                ModelState.AddModelError("CustomError", "Villa already Exists!");
            if (villaCreateDto is null) return BadRequest("villa");

            var model = _mapper.Map<Villa>(villaCreateDto);

            await _dbVilla.CreateAsync(model);

            _response.Result = _mapper.Map<VillaDto>(model);
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtAction(nameof(GetVilla), new { id = model.Id }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErroMessages = [e.ToString()];
        }

        return _response;
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
    {
        try
        {
            if (id == 0) return BadRequest();

            var villa = await _dbVilla.GetAsync(x => x.Id == id);
            if (villa == null) return NotFound();

            await _dbVilla.Remove(villa);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErroMessages = [e.ToString()];
        }

        return _response;
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
    {
        try
        {
            if (villaUpdateDto is null || id != villaUpdateDto.Id) return BadRequest(villaUpdateDto);

            var villa = _mapper.Map<Villa>(villaUpdateDto);
            await _dbVilla.UpdateAsync(villa);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;

            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErroMessages = [e.ToString()];
        }

        return _response;
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patch)
    {
        if (patch == null || id == 0) return BadRequest();


        var villa = await _dbVilla.GetAsync(x => x.Id == id, true);
        if (villa == null) return BadRequest();

        var villaDto = _mapper.Map<VillaUpdateDto>(villa);
        patch.ApplyTo(villaDto, ModelState);
        var model = _mapper.Map<Villa>(villa);
        if (!ModelState.IsValid) return BadRequest();

        await _dbVilla.UpdateAsync(model);

        return NoContent();
    }
}