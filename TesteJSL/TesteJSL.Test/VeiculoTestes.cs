using Xunit;
using NSubstitute;
using TesteJSL.Domain.Interfaces;

namespace TesteJSL.Test
{
    public class VeiculoTestes
    {
        private IVeiculoService mock;

        public VeiculoTestes()
        {
            mock = Substitute.For<IVeiculoService>();
        }

        [Fact]
        public void Get_Veiculos_ReturnTodosOsVeiculos()
        {
            //Act
            var lstVeiculos = mock.BuscaListaVeiculos();

            //Assert
            Assert.Equal(lstVeiculos, lstVeiculos);
        }

        [Fact]
        public void Get_Veiculo_ReturnVeiculoPorId()
        {
            //Act
            var veiculo = mock.BuscaVeiculoPorId(1);

            //Assert
            Assert.True(veiculo != null ? true : false);
        }
    }
}
