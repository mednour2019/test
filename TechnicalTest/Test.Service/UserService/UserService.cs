using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.DTOs;
using Test.Core.Entities;
using Test.Core.Interfaces;

namespace Test.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = _mapper.Map<User>(userDto);
            await userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = await userRepository.GetByIdAsync(id);

            if (user == null) throw new KeyNotFoundException("User not found");

            userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var users = await userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async  Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = await userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = await userRepository.GetByIdAsync(userDto.Id);

            if (user == null) throw new KeyNotFoundException("User not found");

            _mapper.Map(userDto, user);
            userRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
