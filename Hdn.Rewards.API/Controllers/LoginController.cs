using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using Hdn.Rewards.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Hdn.Rewards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private ILoginBusiness _loginBusiness;

        public LoginController(ILogger<LoginController> logger, ILoginBusiness loginBusiness)
        {
            _logger = logger;
            _loginBusiness = loginBusiness;
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (login.Email == "" || login.Password == "") return BadRequest("Preencha todos os campos");
            if (login.Email == null || login.Password == null) return BadRequest("Preencha todos os campos");

            //var user = _loginBusiness.ValidateCredentials(login);
            var token = _loginBusiness.ValidateCredentials(login);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenDto tokenDto)
        {
            if (tokenDto == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidadeCredentials(tokenDto);
            if (token == null) return BadRequest("Invalid client request");

            return Ok(token);

        }
    }

}