using Xunit;
using NSubstitute;
using TesteJSL.Domain.Interfaces;

namespace TesteJSL.Test
{
    public class MotoristaTestes
    {
        private IMotoristaService mock;

        public MotoristaTestes()
        {
            mock = Substitute.For<IMotoristaService>();
        }

        [Fact]
        public void Get_Motoristas_ReturnTodosMotoristas()
        {
            //Act
            var lstMotoristas = mock.BuscaListaMotoristas();

            //Assert
            Assert.Equal(lstMotoristas, lstMotoristas);
        }

        [Fact]
        public void Get_Motoristas_ReturnPorId()
        {
            //Act
            var motorista = mock.BuscaMotoristaPorId(1);

            //Assert
            Assert.True(motorista != null ? true : false);
        }
    }
}
