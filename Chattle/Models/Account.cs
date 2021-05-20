using System;

namespace Chattle.Models
{
    public record Account : IIdentifiable
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public bool IsActive { get; init; } = true;
        public string PasswordHash { get; init; } = String.Empty;
        public string PasswordSalt { get; init; } = String.Empty;
        public AccountGlobalPermission GlobalPermission { get; init; } = AccountGlobalPermission.None;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}