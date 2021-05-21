using System;
using System.Collections.Generic;
using Chattle.ModelsDto;
using MediatR;

namespace Chattle.Queries
{
    public record GetUsersQuery(Guid AccountId, Guid RequesterAccountId) : IRequest<IEnumerable<UserDto>>;
}