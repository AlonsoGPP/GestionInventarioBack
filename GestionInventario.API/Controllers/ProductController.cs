using GestionInventario.Application.Features.Auth.Queries.Login;
using GestionInventario.Application.Features.Products.Commands.CreateProduct;
using GestionInventario.Application.Util;
using GestionInventario.Domain.DTOs.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public ProductController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("/")]
        public async Task<ActionResult<ResponseBase>> Post([FromBody] CreateProductCommand query)
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
