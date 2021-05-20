using System.Threading;
using System.Threading.Tasks;
using Chattle.Database;
using Chattle.Exceptions;
using Chattle.Models;
using Chattle.ModelsDto;
using MediatR;

namespace Chattle.Queries.Handlers
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, AccountDto>
    {
        public GetAccountHandler(DatabaseController databaseController, PermissionChecker permissionChecker, DtoMapper mapper)
        {
            _databaseController = databaseController;
            _permissionChecker = permissionChecker;
            _mapper = mapper;
        }

        private readonly DatabaseController _databaseController;
        private readonly PermissionChecker _permissionChecker;
        private readonly DtoMapper _mapper;

        public Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            if (!_permissionChecker.CanGetAccount(request.RequesterAccountId))
            {
                throw new InsufficientPermissionException<Account>(request.RequesterAccountId);
            }

            var account = _databaseController.Accounts.Get(request.AccountId);
            if (account is null)
            {
                throw new ObjectNotFoundException<Account>(request.RequesterAccountId);
            }

            return Task.FromResult(_mapper.DomainToDto<Account, AccountDto>(account));
        }
    }
}