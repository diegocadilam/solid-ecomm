﻿namespace Ecomm.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email);
    }
}
