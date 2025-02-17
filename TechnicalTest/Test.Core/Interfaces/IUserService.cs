﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.DTOs;

namespace Test.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(Guid id);
    }
}
