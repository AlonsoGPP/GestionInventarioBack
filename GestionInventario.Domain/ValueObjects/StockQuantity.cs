using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.ValueObjects
{

    public class StockQuantity
    {
        public int Value { get; private set; }

        public StockQuantity(int value)
        {
            if (value < 0)
                throw new ArgumentException("La cantidad no puede ser negativa");

            Value = value;
        }

        public void Decrease(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("La reducción debe ser mayor a cero");

            if (Value - amount < 0)
                throw new InvalidOperationException("No hay suficiente inventario");

            Value -= amount;
        }

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El incremento debe ser mayor a cero");

            Value += amount;
        }
    }
}
