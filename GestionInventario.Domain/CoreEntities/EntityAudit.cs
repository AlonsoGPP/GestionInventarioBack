using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.CoreEntities
{
    public abstract class EntityAudit : Entity
    {
        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UpdatedBy { get; set; }
    }
}
