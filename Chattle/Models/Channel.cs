using System;

namespace Chattle.Models
{
    public record Channel : IIdentifiable
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Guid ServerId { get; init; } = Guid.Empty;
        public Guid AuthorId { get; init; } = Guid.Empty;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}