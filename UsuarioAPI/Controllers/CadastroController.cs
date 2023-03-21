using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi_.Data.Request;
using UsuariosApi_.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto_ createDto)
        {
            Result resultado = await _cadastroService.CadastraUsuario(createDto);
            if (resultado.IsFailed)
            {
                return Conflict(resultado.Errors);
            }
            return Ok(resultado.Successes);
        }
        [HttpGet("/ativa")]
        public async Task<IActionResult> AtivaContausuario([FromQuery]AtivaContaRequest request)
        {
            Result resultado = await _cadastroService.AtivaContausuario(request);
            if (resultado.IsFailed)
            {
                return  StatusCode(500);
            }
            return Ok(resultado.Successes);
        }

    }
}
