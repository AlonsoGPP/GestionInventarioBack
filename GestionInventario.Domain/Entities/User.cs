using GestionInventario.Domain.CoreEntities;
using GestionInventario.Domain.Enums;
using GestionInventario.Domain.ValueObjects;

namespace GestionInventario.Domain.Entities
{
    public class User : EntityAudit
    {
        public Username Username { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public UserRole Role { get; private set; }

        private User() { } 

        private User(Username username, Email email, PasswordHash passwordHash, UserRole role)
        {
            Id = Guid.NewGuid();
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Role = role;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(string username, string email, string passwordHash, UserRole role)
        {
            var userName = Username.Create(username);
            var userEmail = Email.Create(email);
            var userPasswordHash = new PasswordHash(passwordHash);

            return new User(userName, userEmail, userPasswordHash, role);
        }

        public void UpdateUsername(string username)
        {
            Username = Username.Create(username);
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(PasswordHash newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
