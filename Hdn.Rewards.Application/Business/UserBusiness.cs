using AutoMapper;
using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using Hdn.Rewards.Domain.Interfaces;
using Hdn.Rewards.Domain.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hdn.Rewards.Application.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserBusiness(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserDto Create(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return _mapper.Map<UserDto>(_repository.Create(user));

        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public UserDto Disable(Guid id)
        {
            return _mapper.Map<UserDto>(_repository.Disable(id));
        }

        public List<UserDto> FindAll()
        {
            var users = _repository.FindAll();
            var usersDto = new List<UserDto>();
            users.ForEach(u => usersDto.Add(_mapper.Map<UserDto>(u)));
            return usersDto;
        }

        public UserDto FindByEmail(string email)
        {
            return _mapper.Map<UserDto>(_repository.FindByEmail(email));
        }

        public UserDto FindByID(Guid id)
        {
            return _mapper.Map<UserDto>(_repository.FindByID(id));
        }

        public UserDto Update(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _repository.Update(user);
            return userDto;
        }
    }
}
