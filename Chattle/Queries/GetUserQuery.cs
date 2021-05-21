using System;
using Chattle.ModelsDto;
using MediatR;

namespace Chattle.Queries
{
    public record GetUserQuery(Guid UserId, Guid RequesterAccountId) : IRequest<UserDto>;
}