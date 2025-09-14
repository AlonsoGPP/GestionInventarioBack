using GestionInventario.Domain.CoreEntities;

namespace GestionInventario.Domain.Entities
{
    public class Notification : EntityAudit
    {
        public string Message { get; private set; }
        public Guid ProductId { get; private set; }
        public bool IsRead { get; private set; }

        public virtual Product Product { get; private set; }

        private Notification() { }

        public Notification(Guid productId, string message)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            IsRead = false;
        }

        public void MarkAsRead()
        {
            IsRead = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
