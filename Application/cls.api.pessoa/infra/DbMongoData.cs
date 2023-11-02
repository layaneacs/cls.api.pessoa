using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.core.model;
using cls.api.pessoa.infra.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace cls.api.pessoa.infra
{
    public class DbMongoData : IDataService
    {
        private readonly MongoSettings _settings;
        public IMongoCollection<Pessoa> _pessoas { get; }

        public DbMongoData(IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;

            var client = new MongoClient(_settings.ConnectionString);
            var db = client.GetDatabase("mongo_acs");

            _pessoas = db.GetCollection<Pessoa>(nameof(Pessoa));
        }

        public async Task<bool> Delete(Guid id)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Id, id);
            var exist = await _pessoas.DeleteOneAsync(filter);
            if (exist.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Pessoa>> GetAll()
        {
            var filter = Builders<Pessoa>.Filter.Empty;
            var all = await _pessoas.Find(filter).ToListAsync();
            return all;
        }

        public async Task<Pessoa?> GetBy(Guid id)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Id, id);
            var exist = await _pessoas.Find(filter).ToListAsync();
            if (exist.Any())
            {
                var resposta = exist.FirstOrDefault();
                return resposta;
            }
            return null;
        }

        public async Task<Pessoa?> GetBy(string email)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Email, email);
            var exist = await _pessoas.Find(filter).ToListAsync();
            if (exist.Any())
            {
                var resposta = exist.FirstOrDefault();
                return resposta;
            }
            return null;
        }

        public async Task<Pessoa?> Save(Pessoa pessoa)
        {
            try
            {
                var filter = Builders<Pessoa>.Filter.Eq(x => x.Email, pessoa.Email);
                var exist = await _pessoas.Find(filter).ToListAsync();
                if(!exist.Any())
                {
                    await _pessoas.InsertOneAsync(pessoa);
                    return pessoa;
                }
                return null;
                
            }
            catch
            {
                return null;
            }
        }

        public async Task<Pessoa?> Update(Guid id, Pessoa pessoa)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Id, id);
            var update = Builders<Pessoa>.Update
                .Set(x => x.Email, pessoa.Email)
                .Set(x => x.DataNascimento, pessoa.DataNascimento)
                .Set(x => x.Nome, pessoa.Nome)
                .Set(x => x.Sobrenome, pessoa.Sobrenome)
                .Set(x => x.Telefone, pessoa.Telefone);

            var resposta = await _pessoas.UpdateOneAsync(filter, update);
            if (resposta?.ModifiedCount > 0)
            {
                return pessoa;
            }
            return null;
        }
    }
}
