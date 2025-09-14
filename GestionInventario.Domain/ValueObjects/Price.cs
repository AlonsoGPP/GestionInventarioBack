using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.ValueObjects
{
    public class Price
    {
        public decimal Value { get; private set; }

        public Price(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("El precio no puede ser negativo");

            Value = value;
        }

        public override string ToString() => Value.ToString("C2");
    }
}
