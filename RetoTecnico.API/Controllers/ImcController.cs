using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetoTecnico.Application.DTOs;
using RetoTecnico.Application.Services;

namespace RetoTecnico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Token JWT
    public class ImcController : ControllerBase
    {
        private readonly ImcService _imcService;

        public ImcController(ImcService imcService)
        {
            _imcService = imcService;
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularImc([FromBody] ImcRequestDto request)
        {
            var resultado = await _imcService.CalcularYGuardarImcAsync(request);
            return Ok(resultado);
        }
    }
}