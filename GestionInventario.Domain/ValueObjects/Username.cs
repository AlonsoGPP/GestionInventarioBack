using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.ValueObjects
{
    public record Username
    {
        public string Value { get; }

        private Username(string value)
        {
            Value = value;
        }

        public static Username Create(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty");

            if (username.Length < 3)
                throw new ArgumentException("Username must be at least 3 characters long");

            if (username.Length > 50)
                throw new ArgumentException("Username cannot exceed 50 characters");

            return new Username(username.Trim());
        }

        public override string ToString() => Value;

        public static implicit operator string(Username username) => username.Value;
    }
}
