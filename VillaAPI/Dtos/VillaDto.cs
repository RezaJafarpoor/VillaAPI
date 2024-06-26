namespace VillaAPI.Dtos;

public record VillaDto
{
    public int  Id { get; set; }
    public string Name { get; set; }
    public int Occupancy { get; set; }
    public int Sqft { get; set; }
}