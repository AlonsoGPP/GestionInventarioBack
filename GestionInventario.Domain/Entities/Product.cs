using GestionInventario.Domain.CoreEntities;
using GestionInventario.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.Entities
{
    public class Product : EntityAudit
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Price Price { get; private set; }
        public StockQuantity Quantity { get; private set; }
        public Guid CategoryId { get; private set; }

        public virtual Category? Category { get; private set; }

        private Product() { }

        private Product(string name, string? description, Price price, StockQuantity quantity, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacío");

            Id = Guid.NewGuid();
            Name = name.Trim();
            Description = description?.Trim();
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public static Product Create(string name, string? description, decimal price, int quantity, Guid categoryId)
        {
            var voPrice = new Price(price);
            var voQuantity = new StockQuantity(quantity);
            return new Product(name, description, voPrice, voQuantity, categoryId);
        }

        public void UpdateDetails(string name, string? description, decimal price, Guid categoryId, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacío");

            Name = name.Trim();
            Description = description?.Trim();
            Price = new Price(price);
            Quantity = new StockQuantity(quantity);
            CategoryId = categoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool HasLowStock(int threshold = 5) => Quantity.Value < threshold;
    }
}
