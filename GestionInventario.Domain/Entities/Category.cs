using GestionInventario.Domain.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.Entities
{
    public class Category : EntityAudit
    {
        public string Name { get; private set; }

        private Category() { }

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío");

            Id = Guid.NewGuid();
            Name = name.Trim();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
