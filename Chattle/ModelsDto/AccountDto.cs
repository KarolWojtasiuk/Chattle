using System;
using Chattle.Models;

namespace Chattle.ModelsDto
{
    public record AccountDto : IDtoObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public bool IsActive { get; init; } = true;
        public AccountGlobalPermission GlobalPermission { get; init; } = AccountGlobalPermission.None;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}