﻿using System.Text.RegularExpressions;

namespace GestionInventario.Domain.ValueObjects
{
    public record Email
    {
        private static readonly Regex EmailRegex = new(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled);

        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty");

            if (!EmailRegex.IsMatch(email))
                throw new ArgumentException("Invalid email format");

            return new Email(email.ToLowerInvariant());
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;
    }
}
