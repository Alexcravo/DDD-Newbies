using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hdn.Rewards.Domain.Entities
{
    [Table("Users")]
    public class User
    {

        [Key]
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string Role { get; set; }

        public string AccessType { get; set; }
        public DateTime LastAccess { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }


    }
}
