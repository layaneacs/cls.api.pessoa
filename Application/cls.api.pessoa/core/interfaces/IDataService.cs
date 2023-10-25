using cls.api.pessoa.core.model;

namespace cls.api.pessoa.core.interfaces
{
    public interface IDataService
    {
        public Pessoa? Save(Pessoa pessoa);
        public Pessoa? GetBy(Guid id);
        public Pessoa? GetBy(string email);
        public List<Pessoa> GetAll();
    }
}
