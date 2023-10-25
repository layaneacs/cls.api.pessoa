using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.core.model;
using Microsoft.AspNetCore.Mvc;

namespace cls.api.pessoa.controller
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IDataService _service;
        public PessoaController(IDataService service)
        {
            _service= service;
        }

        [HttpGet]
        public async Task<PessoaOutput<List<Pessoa>>> Get()
        {
            var outputValue = new PessoaOutput<List<Pessoa>>();

            var lista = _service?.GetAll() ?? new();
            outputValue.Data = lista;
            return await Task.FromResult(outputValue);
        }

        [HttpPost]
        public async Task<PessoaOutput<Pessoa>> Save(Pessoa pessoa)
        {
            var outputValue = new PessoaOutput<Pessoa>();

            var pessoaCreated = _service?.Save(pessoa) ?? new();

            if (pessoaCreated.Nome is not null)
            {
                outputValue.Data = pessoaCreated;
                return await Task.FromResult(outputValue);
            }

            outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Usuário não foi criado.");
            return await Task.FromResult(outputValue);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<PessoaOutput<Pessoa>> Get(string id)
        {
            var outputValue = new PessoaOutput<Pessoa>();

            if (!Guid.TryParse(id, out Guid idByuser))
            {
                outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Id com o formato inválido.");
                return await Task.FromResult(outputValue);
            }

            var pessoa = _service?.GetBy(idByuser);

            if (pessoa is not null)
            {
                outputValue.Data = pessoa;
                return await Task.FromResult(outputValue);
            }

            outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Usuário não encotrado.");
            return await Task.FromResult(outputValue);
        }
    }
}
