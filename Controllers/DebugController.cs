namespace MissingAadProvider.Controllers;

using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

[ApiController]
[Route("[controller]")]
public sealed class DebugController : ControllerBase
{
    [HttpGet("environment")]
    public IEnumerable<DictionaryEntry> GetEnvironment()
    {
        return Environment.GetEnvironmentVariables()
            .Cast<DictionaryEntry>()
            .OrderBy(x => x.Key);
    }

    [HttpGet("auth-me")]
    public object GetAuth()
    {
        return new
        {
            Name = User.Identity is { IsAuthenticated: true }
                ? User.Identity.Name
                : "not authenticated",
            Email = User.FindFirstValue(ClaimTypes.Email),
            Iss = User.FindFirstValue("iss"),
            Ver = User.FindFirstValue("ver"),
        };
    }

    [HttpGet("x-headers")]
    public IEnumerable<KeyValuePair<string, StringValues>> GetXHeaders()
    {
        return Request.Headers
            .Where(x => x.Key.StartsWith("X-", StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Key);
    }

    [HttpGet("headers")]
    public IEnumerable<KeyValuePair<string, StringValues>> GetHeaders()
    {
        return Request.Headers.OrderBy(x => x.Key);
    }
}