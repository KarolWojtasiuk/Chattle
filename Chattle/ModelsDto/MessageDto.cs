using System;
using Chattle.Models;

namespace Chattle.ModelsDto
{
    public record MessageDto : IDtoObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Content { get; init; } = String.Empty;
        public Guid ChannelId { get; init; } = Guid.Empty;
        public Guid AuthorId { get; init; } = Guid.Empty;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}