using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hdn.Rewards.Domain.Interfaces.Business
{
    public interface IUserBusiness
    {
        UserDto Create(UserDto user);
        UserDto FindByID(Guid id);
        UserDto FindByEmail(string email);
        List<UserDto> FindAll();
        UserDto Update(UserDto user);
        void Delete(Guid id);
        UserDto Disable(Guid id);

    }
}
