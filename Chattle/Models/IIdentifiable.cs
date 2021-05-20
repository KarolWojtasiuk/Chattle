using System;

namespace Chattle.Models
{
    public interface IIdentifiable
    {
        public Guid Id { get; }
    }
}