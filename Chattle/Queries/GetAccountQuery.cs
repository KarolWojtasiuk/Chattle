using System;
using Chattle.ModelsDto;
using MediatR;

namespace Chattle.Queries
{
    public record GetAccountQuery(Guid AccountId, Guid RequesterAccountId) : IRequest<AccountDto>;
}