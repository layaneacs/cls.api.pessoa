using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.core.model;

namespace cls.api.pessoa.infra
{
    public class FakeData : IDataService
    {
        private static List<Pessoa> pessoas = new List<Pessoa>()
        {
            new Pessoa()
            {
                DataNascimento = DateTime.Now.AddYears(-18),
                Email = "pessoa@admin.com.br",
                Nome = "ADMIN",
                Sobrenome = "PLATAFORMA",
                Telefone = "24242828"
            },
            new Pessoa()
            {
                DataNascimento = DateTime.Now.AddYears(-21),
                Email = "pessoa2@admin.com.br",
                Nome = "ADMIN2",
                Sobrenome = "PLATAFORMA2",
                Telefone = "24242828"
            }
        };

        public List<Pessoa> GetAll()
        {
            return pessoas;           
        }

        public Pessoa? GetBy(Guid id)
        {
            return pessoas.FirstOrDefault(p => p.Id == id);
        }

        public Pessoa? GetBy(string email)
        {
            return pessoas.FirstOrDefault(p => p.Email == email);
        }

        public Pessoa? Save(Pessoa pessoa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pessoa.Email) || GetBy(pessoa.Email) is not null)
                {
                    return null; 
                }
                pessoas.Add(pessoa);
                return pessoa;
            }
            catch
            {
                return null;
            }
        }
    }
}
