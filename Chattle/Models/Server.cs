using System;

namespace Chattle.Models
{
    public record Server : IIdentifiable
    {
        public Guid Id { get; internal init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Uri? ImageUri { get; init; } = null;
        public Guid OwnerId { get; internal init; } = Guid.Empty;
        public DateTime CreationDate { get; internal init; } = DateTime.UtcNow;
    }
}