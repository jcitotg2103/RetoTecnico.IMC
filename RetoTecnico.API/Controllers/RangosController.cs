using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetoTecnico.Application.Interfaces;
using RetoTecnico.Domain.Entities;

namespace RetoTecnico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RangosController : ControllerBase
    {
        private readonly IRangoRepository _repository;
        public RangosController(IRangoRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repository.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(RangoImc rango)
        {
            await _repository.AddAsync(rango);
            return Ok(new { mensaje = "Rango creado exitosamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RangoImc rango)
        {
            rango.Id = id;
            await _repository.UpdateAsync(rango);
            return Ok(new { mensaje = "Rango actualizado" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok(new { mensaje = "Rango eliminado" });
        }
    }
}