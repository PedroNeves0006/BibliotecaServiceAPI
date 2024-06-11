using BibliotecaServiceAPI.Models;
using BibliotecaServiceAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaServiceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoRepositorio _emprestimoRepositorio;

        public EmprestimoController(IEmprestimoRepositorio emprestimoRepositorio)
        {
            _emprestimoRepositorio = emprestimoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmprestimoModel>>> BuscarTodosEmprestimos()
        {
            List<EmprestimoModel> emprestimos = await _emprestimoRepositorio.BuscarTodosEmprestimos();

            return Ok(emprestimos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EmprestimoModel>>> BuscarPorID(int id)
        {
            EmprestimoModel emprestimo = await _emprestimoRepositorio.BuscarPorId(id);
            return Ok(emprestimo);
        }

        [HttpPost]
        public async Task<ActionResult<EmprestimoModel>> Cadastrar([FromBody]EmprestimoModel emprestimoModel)
        {
            EmprestimoModel emprestimo = await _emprestimoRepositorio.Adicionar(emprestimoModel);
            return Ok(emprestimo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<EmprestimoModel>>> Atualizar([FromBody] EmprestimoModel emprestimoModel, int id)
        {
            emprestimoModel.Id = id;
            EmprestimoModel emprestimo = await _emprestimoRepositorio.Atualizar(emprestimoModel, id);
            return Ok(emprestimo);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<EmprestimoModel>>> Apagar(int id)
        {
            bool apagado = await _emprestimoRepositorio.Apagar(id);
            return Ok(apagado);
        }

        [HttpPost("{id}/devolucao")]
        public async Task<ActionResult<EmprestimoModel>> RegistrarDevolucao(int id)
        {
            try
            {
                EmprestimoModel emprestimo = await _emprestimoRepositorio.RegistrarDevolucao(id);
                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
