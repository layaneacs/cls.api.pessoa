using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.core.model;
using Moq;

namespace cls.api.pessoa.tests.core
{
    public class PessoaTests
    {
        private Mock<IDataService> dataService = new Mock<IDataService>();

        [Fact]
        public void QuandoSalvarUmaPessoaValida_EntaoDeveRetornarAPessoaComId()
        {
            var pessoa = new Pessoa()
            {
                DataNascimento = DateTime.Now.AddYears(-21),
                Email = "pessoa3@admin.com.br",
                Nome = "ADMIN2",
                Sobrenome = "PLATAFORMA",
                Telefone = "24242828"
            };

            Assert.True(Guid.Empty != pessoa.Id);
        }
    }
}
