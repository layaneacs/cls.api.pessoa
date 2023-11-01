﻿using cls.api.pessoa.controller;
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
                .Returns(() => null);

            var controller = new PessoaController(dataService.Object);

            var result = controller.Save(It.IsAny<Pessoa>()).Result;

            Assert.True(result.Notificacao.HasNotificacao);
        }

        [Fact]
        public void QuandoSalvarUmaPessoaValida_EntaoOutputDeveRetornarSemNotificacao()
        {
            dataService
                .Setup(x => x.Save(It.IsAny<Pessoa>()))
                .Returns(() => new Pessoa());

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
                .Returns(() => new Pessoa());

            var controller = new PessoaController(dataService.Object);

            var result = controller.Get(idValid.ToString()).Result;

            Assert.False(result.Notificacao.HasNotificacao);
            Assert.NotNull(result.Data);
        }

    }
}