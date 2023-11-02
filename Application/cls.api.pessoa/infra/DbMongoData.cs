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

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pessoa>> GetAll()
        {
            var filter = Builders<Pessoa>.Filter.Empty;
            var all = await _pessoas.Find(filter).ToListAsync();
            return all;
        }

        public Pessoa? GetBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public Pessoa? GetBy(string email)
        {
            throw new NotImplementedException();
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

        public Pessoa? Update(Guid id, Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
