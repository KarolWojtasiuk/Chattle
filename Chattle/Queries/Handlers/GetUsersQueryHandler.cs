using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chattle.Database;
using Chattle.Exceptions;
using Chattle.Models;
using Chattle.ModelsDto;
using MediatR;

namespace Chattle.Queries.Handlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        public GetUsersQueryHandler(DatabaseController databaseController, PermissionChecker permissionChecker, DtoMapper mapper)
        {
            _databaseController = databaseController;
            _permissionChecker = permissionChecker;
            _mapper = mapper;
        }

        private readonly DatabaseController _databaseController;
        private readonly PermissionChecker _permissionChecker;
        private readonly DtoMapper _mapper;

        public Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var requesterAccount = _databaseController.Accounts.Get(request.RequesterAccountId);
            if (requesterAccount is null)
            {
                throw new ObjectNotFoundException<Account>(request.RequesterAccountId);
            }

            if (!_permissionChecker.CanGetUser(requesterAccount))
            {
                throw new InsufficientPermissionException<Account>(requesterAccount.Id);
            }

            var users = _databaseController.Users.FindMany(u => u.AccountId == request.AccountId);
            var usersDto = users.Select(u => _mapper.DomainToDto<User, UserDto>(u));

            return Task.FromResult(usersDto);
        }
    }
}