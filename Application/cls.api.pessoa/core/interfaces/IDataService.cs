using cls.api.pessoa.core.model;

namespace cls.api.pessoa.core.interfaces
{
    public interface IDataService
    {
        public Task<Pessoa?> Save(Pessoa pessoa);
        public Pessoa? GetBy(Guid id);
        public Pessoa? Update(Guid id, Pessoa pessoa);
        public bool Delete(Guid id);
        public Pessoa? GetBy(string email);
        public Task<List<Pessoa>> GetAll();
    }
}
