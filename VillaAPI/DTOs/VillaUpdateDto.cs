using System.ComponentModel.DataAnnotations;

namespace VillaAPI.DTOs;

public record VillaUpdateDto
{
    [Required]
    public int  Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Occupancy { get; set; }
    [Required]
    public int Sqft { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
    public string Details { get; set; }
    [Required]
    public double Rate { get; set; }
}