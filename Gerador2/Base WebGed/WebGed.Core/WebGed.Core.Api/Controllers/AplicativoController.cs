using Microsoft.AspNetCore.Mvc;
using WebGed.Core.Api.Configuration.AutoMapper;
using WebGed.Core.Api.Configuration.Validation;
using WebGed.Core.Api.Models;
using WebGed.Core.Dominio.Core;
using WebGed.Core.Dominio.IRepositorio;
using WebGed.Core.Dominio.Notificacoes.Interfaces;

namespace WebGed.Core.Api.Controllers
{
    [Route("api/aplicativos")]
    [ApiController]
    public class AplicativoController : ControllerBase
    {
        private readonly IRepositorioAplicativo _repositorioAplicativo;
        private readonly AplicativoModelValidator _aplicativoModelValidator;
        private readonly INotificador _notificador;
        public AplicativoController(
            IRepositorioAplicativo repositorioAplicativo,
            AplicativoModelValidator aplicativoModelValidator,
            INotificador notificador)
        {
            _repositorioAplicativo = repositorioAplicativo;
            _aplicativoModelValidator = aplicativoModelValidator;
            _notificador = notificador;
        }

        [HttpGet("{pagina:int}/listar")]
        public async Task<PaginacaoModel<AplicativoModel>> List([FromRoute] int pagina)
        {
            var entidades = await _repositorioAplicativo.ListarAsync(pagina: pagina);
            return entidades.Map<AplicativoModel, Aplicativo>();
        }

        [HttpGet("{id:int}")]
        public async Task<AplicativoModel> Get([FromRoute] int id)
        {
            var entidade = await _repositorioAplicativo.ObterAsync(id);
            return entidade.Map<AplicativoModel>();
        }

        [HttpPost]
        public async Task<ActionResult<AplicativoModel>> Post([FromBody] AplicativoModel model)
        {
            var results = _aplicativoModelValidator.Validate(model);
            if (!results.IsValid)
            {
                _notificador.Adicionar(results);
            }

            var entidade = model.Map<Aplicativo>();
            await _repositorioAplicativo.SalvarAsync(entidade);
            await _repositorioAplicativo.CommitAsync();
            return entidade.Map<AplicativoModel>();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repositorioAplicativo.ExluirAsync(id);
            await _repositorioAplicativo.CommitAsync();
            return Ok();
        }
    }
}
