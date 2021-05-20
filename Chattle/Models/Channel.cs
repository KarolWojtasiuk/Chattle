using System;

namespace Chattle.Models
{
    public record Channel : IIdentifiable
    {
        public Guid Id { get; internal init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Guid ServerId { get; internal init; } = Guid.Empty;
        public Guid AuthorId { get; internal init; } = Guid.Empty;
        public DateTime CreationDate { get; internal init; } = DateTime.UtcNow;
    }
}