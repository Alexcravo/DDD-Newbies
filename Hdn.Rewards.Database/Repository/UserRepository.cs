using Hdn.Rewards.Database.Context;
using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using Hdn.Rewards.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hdn.Rewards.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private PostgreSQLContext _context;

        public UserRepository(PostgreSQLContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();

                var passwithHash = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
                user.Password = passwithHash;
                user.IsActive = true;

                _context.Add(user);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }

            return user;
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

            if (user != null)
            {
                try
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public User Disable(Guid id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

            if (user != null)
            {
                try
                {
                    var userTemp = user;
                    userTemp.IsActive = false;

                    _context.Entry(user).CurrentValues.SetValues(userTemp);
                    _context.SaveChanges();

                    return userTemp;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }

        }

        public List<User> FindAll()
        {
            return _context.Users.ToList();
        }

        public User FindByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email.Equals(email));
        }

        public User FindByID(Guid id)
        {
            return _context.Users.SingleOrDefault(u => u.Id.Equals(id));
        }

        public bool IsActive(Guid id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

            return user.IsActive;

        }

        public User Update(User user)
        {
            var result = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }

            return user;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
