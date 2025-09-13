using GestionInventario.Domain.ValueObjects;

namespace GestionInventario.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public string HashedPassword { get; private set; }
        public string Role { get; private set; } = "Empleado";

        private User() { }

        public User(string username, Email email, string password, string role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            HashedPassword = password;
            Role = role;
        }
    }
}
