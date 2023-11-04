using cls.api.pessoa.core.model;

namespace cls.api.pessoa.core.interfaces
{
    public interface IDataService
    {
        public Task<Pessoa?> Save(Pessoa pessoa);
        public Task<Pessoa?> GetBy(Guid id);
        public Task<Pessoa?> Update(Guid id, Pessoa pessoa);
        public Task<bool> Delete(Guid id);
        public Task<Pessoa?> GetBy(string email);
        public Task<List<Pessoa>> GetAll();
    }
}
