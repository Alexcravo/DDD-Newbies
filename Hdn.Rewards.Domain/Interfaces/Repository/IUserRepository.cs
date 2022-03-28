using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hdn.Rewards.Domain.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
        User FindByID(Guid id);
        User FindByEmail(string email);
        List<User> FindAll();
        User Update(User user);
        void Delete(Guid id);
        User Disable(Guid id);
        bool IsActive(Guid id);

    }
}
