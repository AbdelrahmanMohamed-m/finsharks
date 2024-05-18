
using api.Models;

namespace api.ControlLayer.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}