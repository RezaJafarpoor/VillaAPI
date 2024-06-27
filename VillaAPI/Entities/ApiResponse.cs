using System.Net;

namespace VillaAPI.Entities;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> ErroMessages { get; set; }
    public object Result { get; set; } 
}