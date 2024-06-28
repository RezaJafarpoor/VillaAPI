using System.ComponentModel.DataAnnotations;

namespace VillaAPI.DTOs;

public class VillaNumberDto
{
    [Required]
    public int  VillaNo { get; set; }
    public string SpecialDetails { get; set; }
}