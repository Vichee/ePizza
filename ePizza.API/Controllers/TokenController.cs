using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public TokenController(ITokenGeneratorService tokenGeneratorService)
        {
            this._tokenGeneratorService = tokenGeneratorService;
        }


        [HttpGet]
        [Route("{userName}/{password}")]
        public async Task<IActionResult> GetToken(string userName, string password)
        {

            var response = await _tokenGeneratorService.GenerateTokenAsync(userName, password);

            return Ok(response);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {

            var response = await _tokenGeneratorService.GenerateRefreshTokenAsync(request);

            return Ok(response);
        }
    }
}
