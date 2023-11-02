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

            var lista = await _service.GetAll() ?? new();
            outputValue.Data = lista;
            return await Task.FromResult(outputValue);
        }

        [HttpPost]
        public async Task<PessoaOutput<Pessoa>> Save(Pessoa pessoa)
        {
            var outputValue = new PessoaOutput<Pessoa>();

            var pessoaCreated = await _service.Save(pessoa);

            if (pessoaCreated is not null)
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

            var pessoa = await _service.GetBy(idByuser);

            if (pessoa is not null)
            {
                outputValue.Data = pessoa;
                return await Task.FromResult(outputValue);
            }

            outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Usuário não encotrado.");
            return await Task.FromResult(outputValue);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<PessoaOutput<string>> Delete(string id)
        {
            var outputValue = new PessoaOutput<string>();

            if (!Guid.TryParse(id, out Guid idByuser))
            {
                outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Id com o formato inválido.");
                return await Task.FromResult(outputValue);
            }

            var deleted = await _service.Delete(idByuser);

            if (deleted)
            {
                outputValue.Data = "Deletado com sucesso";
                return await Task.FromResult(outputValue);
            }

            outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Usuário não encotrado.");
            return await Task.FromResult(outputValue);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<PessoaOutput<Pessoa>> Update(string id, Pessoa pessoa)
        {
            var outputValue = new PessoaOutput<Pessoa>();

            if (!Guid.TryParse(id, out Guid idByuser))
            {
                outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Id com o formato inválido.");
                return await Task.FromResult(outputValue);
            }

            var updated = await _service.Update(idByuser, pessoa);

            if (updated is not null)
            {
                outputValue.Data = updated;
                return await Task.FromResult(outputValue);
            }

            outputValue.Notificacao.AddNotificacao(Guid.NewGuid().ToString(), "Usuário não encotrado.");
            return await Task.FromResult(outputValue);
        }
    }
}
