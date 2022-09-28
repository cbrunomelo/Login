using System.Security.Claims;
using Models;

namespace Extensions;


public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email)
        };
  
        return result;
    }
}