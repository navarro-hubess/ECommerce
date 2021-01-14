using DecimalMath;
using System;

namespace Domain
{
    public class Financial
    {
        public int Tempo { get; set; }
        public decimal ValorInicial { get; set; }

        public decimal CalculaJuros(decimal valorInicial, int tempo, decimal juros)
        {
            decimal valorFinal = Truncar(DecimalEx.Pow((1 + juros), tempo) * valorInicial);
            return valorFinal;
        }

        private decimal Truncar(decimal valor)
        {
            valor *= 100;
            valor = Math.Truncate(valor);
            valor /= 100;

            return valor;
        }
    }
}
