using GestionInventario.Application.Features.Auth.Commands.RegisterUser;
using GestionInventario.Application.Features.Auth.Queries.Login;
using GestionInventario.Application.Util;
using GestionInventario.Domain.DTOs.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseBase>> RegisterUser([FromBody] RegisterUserCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "", ex.Message);
                return ResponseUtil.BadRequest(ex.Message.ToString());
            }
        }        
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseBase>> Login([FromBody] LoginQuery query)
        {
            try
            {
                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "", ex.Message);
                return ResponseUtil.BadRequest(ex.Message.ToString());
            }
        }
    }
}
