using System;
using System.Text.Json.Serialization;

namespace Hdn.Rewards.Domain.DTO
{
    public class UserDto
    {
        [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
