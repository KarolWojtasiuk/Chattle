using System;

namespace Chattle.ModelsDto
{
    public record ChannelDto : IDtoObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Guid ServerId { get; init; } = Guid.Empty;
        public Guid AuthorId { get; init; } = Guid.Empty;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}