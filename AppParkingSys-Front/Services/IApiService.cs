﻿using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Services
{
    public interface IApiService
    {
        Task<User?> LoginAsync(string email, string password);
    }
}