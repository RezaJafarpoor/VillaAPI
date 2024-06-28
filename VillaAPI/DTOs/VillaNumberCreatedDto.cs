using System.ComponentModel.DataAnnotations;

namespace VillaAPI.DTOs;

public class VillaNumberCreatedDto
{
    [Required]
    public int  VillaNo { get; set; }
    public string SpecialDetails { get; set; }
}