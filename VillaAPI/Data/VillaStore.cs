using VillaAPI.Dtos;

namespace VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDto> villaList =
    [
        new VillaDto { Id = 1, Name = "Pool View" },
        new VillaDto { Id = 2, Name = "Beach View" }
    ];
}