using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTest
{
    public class CalculoJurosUnitTest
    {
        [Fact]
        public void CalculoDeveRetornarUmDeterminadoValor()
        {
            Financial financial = new Financial();
            var resultado = financial.CalculaJuros(100, 5, 0.01m);
            //Verifica se o resultado é igual a 105,10        
            Assert.Equal(105.10m, resultado);
        }
    }
}
