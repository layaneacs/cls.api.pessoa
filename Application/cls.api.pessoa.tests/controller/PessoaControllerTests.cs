using cls.api.pessoa.controller;
using cls.api.pessoa.core.interfaces;
using cls.api.pessoa.core.model;
using Moq;

namespace cls.api.pessoa.tests.controller
{
    public class PessoaControllerTests
    {
        private Mock<IDataService> dataService = new Mock<IDataService>();

        [Fact]
        public void QuandoSalvarUmaPessoaInvalida_EntaoOutputDeveRetornarNotificacao()
        {  
            dataService
                .Setup(x => x.Save(It.IsAny<Pessoa>()))
                .ReturnsAsync(() => null);

            var controller = new PessoaController(dataService.Object);

            var result = controller.Save(It.IsAny<Pessoa>()).Result;

            Assert.True(result.Notificacao.HasNotificacao);
        }

        [Fact]
        public void QuandoSalvarUmaPessoaValida_EntaoOutputDeveRetornarSemNotificacao()
        {
            dataService
                .Setup(x => x.Save(It.IsAny<Pessoa>()))
                .ReturnsAsync(() => new Pessoa());

            var controller = new PessoaController(dataService.Object);

            var result = controller.Save(It.IsAny<Pessoa>()).Result;

            Assert.False(result.Notificacao.HasNotificacao);
        }

        [Fact]
        public void QuandoBuscaPorIdValido_EntaoOutputDeveRetornarSemNotificacaoEDataNaoNulo()
        {
            var idValid = Guid.NewGuid();

            dataService
                .Setup(x => x.GetBy(idValid))
                .ReturnsAsync(() => new Pessoa());

            var controller = new PessoaController(dataService.Object);

            var result = controller.Get(idValid.ToString()).Result;

            Assert.False(result.Notificacao.HasNotificacao);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void QuandoDeletePorIdValido_EntaoOutputDeveRetornarSemNotificacaoEDataNaoNulo()
        {
            var idValid = Guid.NewGuid();

            dataService
                .Setup(x => x.Delete(idValid))
                .ReturnsAsync(() => true);

            var controller = new PessoaController(dataService.Object);

            var result = controller.Delete(idValid.ToString()).Result;

            Assert.False(result.Notificacao.HasNotificacao);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void QuandoUpdatePorIdValido_EntaoOutputDeveRetornarSemNotificacaoEDataNaoNulo()
        {
            var idValid = Guid.NewGuid();

            dataService
                .Setup(x => x.Update(idValid, It.IsAny<Pessoa>()))
                .ReturnsAsync(() => new Pessoa());

            var controller = new PessoaController(dataService.Object);

            var result = controller.Update(idValid.ToString(), It.IsAny<Pessoa>()).Result;

            Assert.False(result.Notificacao.HasNotificacao);
            Assert.NotNull(result.Data);
        }

    }
}
