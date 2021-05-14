using System;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record AccountEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = String.Empty;
        public bool IsActive { get; init; } = false;
        public string PasswordHash { get; init; } = String.Empty;
        public string PasswordSalt { get; init; } = String.Empty;
        public AccountGlobalPermission GlobalPermission { get; init; } = AccountGlobalPermission.None;
        public DateTime CreationDate { get; init; } = DateTime.MinValue;
    }
}