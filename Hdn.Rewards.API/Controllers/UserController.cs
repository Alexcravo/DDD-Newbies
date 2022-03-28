using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using Hdn.Rewards.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Hdn.Rewards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //Dependency injection variables
        private readonly ILogger<UserController> _logger;
        private IUserBusiness _UserBusiness;

        public UserController(ILogger<UserController> logger, IUserBusiness userBusiness)
        {
            _logger = logger;
            _UserBusiness = userBusiness;
        }

        //routes

        //route to create a user
        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null) return BadRequest();
            
            if (_UserBusiness.FindByEmail(userDto.Email) != null) return BadRequest("Email já está cadastrado");

            var result = _UserBusiness.Create(userDto);

            return Created("~/api/user/" + result.Id,result);

        }

        //route to find user by id
        [HttpGet("{id}")]
        public IActionResult FindByID(Guid id)
        {
            var user = _UserBusiness.FindByID(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        //route to find all users
        [HttpGet()]
        [Authorize("Bearer")]
        public IActionResult FindAll()
        {
            return Ok(_UserBusiness.FindAll());
        }

        //route to update a user
        [HttpPut()]
        public IActionResult UpdateUser([FromBody] UserDto user)
        {
            if (user == null) return BadRequest();
            return Ok(_UserBusiness.Update(user));
        }

        //route to delete a user
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            _UserBusiness.Delete(id);
            return NoContent();
        }

        //route to disable a user
        [HttpPut("{id}")]
        public IActionResult DisableUser(Guid id)
        {
            _UserBusiness.Disable(id);
            return NoContent();
        }

    }
}
