using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.ValueObjects
{
    public sealed class Username
    {
        public string Value { get; }

        private Username(string value)
        {
            Value = value;
        }

        public static Username Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Username cannot be empty");

            value = value.Trim();

            if (value.Length < 3)
                throw new ArgumentException("Username must have at least 3 characters");

            if (value.Length > 20)
                throw new ArgumentException("Username cannot exceed 20 characters");

            if (value.Contains(" "))
                throw new ArgumentException("Username cannot contain spaces");

            return new Username(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object obj) =>
            obj is Username other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() =>
            Value.ToLowerInvariant().GetHashCode();
    }
}
