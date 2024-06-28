using System.ComponentModel.DataAnnotations;

namespace VillaAPI.DTOs;

public class VillaNumberUpdateDto
{
    [Required]
    public int  VillaNo { get; set; }
    public string SpecialDetails { get; set; }
}