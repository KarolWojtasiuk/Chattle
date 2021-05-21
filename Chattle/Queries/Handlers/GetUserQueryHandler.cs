using System.Threading;
using System.Threading.Tasks;
using Chattle.Database;
using Chattle.Exceptions;
using Chattle.Models;
using Chattle.ModelsDto;
using MediatR;

namespace Chattle.Queries.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        public GetUserQueryHandler(DatabaseController databaseController, PermissionChecker permissionChecker, DtoMapper mapper)
        {
            _databaseController = databaseController;
            _permissionChecker = permissionChecker;
            _mapper = mapper;
        }

        private readonly DatabaseController _databaseController;
        private readonly PermissionChecker _permissionChecker;
        private readonly DtoMapper _mapper;

        public Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
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

            var user = _databaseController.Users.Get(request.UserId);
            if (user is null)
            {
                throw new ObjectNotFoundException<User>(request.UserId);
            }

            return Task.FromResult(_mapper.DomainToDto<User, UserDto>(user));
        }
    }
}