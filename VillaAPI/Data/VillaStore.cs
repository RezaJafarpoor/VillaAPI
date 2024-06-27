using VillaAPI.DTOs;

namespace VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDto> villaList =
    [
        new VillaDto { Id = 1, Name = "Pool View", Sqft = 100, Occupancy = 4},
        new VillaDto { Id = 2, Name = "Beach View", Sqft =200, Occupancy = 3}
    ];
}