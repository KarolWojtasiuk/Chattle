using System;

namespace Chattle.Models
{
    public record Message : IIdentifiable
    {
        public Guid Id { get; internal init; } = Guid.NewGuid();
        public string Content { get; init; } = String.Empty;
        public Guid ChannelId { get; internal init; } = Guid.Empty;
        public Guid AuthorId { get; internal init; } = Guid.Empty;
        public DateTime CreationDate { get; internal init; } = DateTime.UtcNow;
    }
}