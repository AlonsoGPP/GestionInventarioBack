using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.ValueObjects
{
    public record PasswordHash
    {
        public string Value { get; init; }

        public PasswordHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password hash cannot be null or empty", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;

        public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
    }
}
