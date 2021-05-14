using System;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record AccountEntity : IEntity
    {
        public Guid Id { get; private init; }
        public string Name { get; init; }
        public bool IsActive { get; init; }
        public string PasswordHash { get; init; }
        public string PasswordSalt { get; init; }
        public AccountGlobalPermission GlobalPermission { get; init; }
        public DateTime CreationDate { get; private init; }

        public AccountEntity(string name, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsActive = isActive;
            PasswordHash = String.Empty;
            PasswordSalt = String.Empty;
            GlobalPermission = AccountGlobalPermission.None;
            CreationDate = DateTime.UtcNow;
        }
    }
}