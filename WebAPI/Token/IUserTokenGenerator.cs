using System;

namespace WebAPI.Token
{
    public interface IUserTokenGenerator
    {
        string GenerateToken(int userId);
    }
}
