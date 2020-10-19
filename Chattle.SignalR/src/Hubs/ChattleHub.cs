using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chattle.SignalR
{
    public class ChattleHub : Hub
    {
        private readonly Chattle _chattle;

        public ChattleHub(Chattle chattle)
        {
            _chattle = chattle;
        }

        [AllowAnonymous]
        public Task CreateAccount(string username, string password)
        {
            try
            {
                var account = new Account(username);
                _chattle.AccountController.Create(account);
                _chattle.AccountController.SetPassword(account.Id, password, account.Id);
                return Clients.Caller.SendAsync(nameof(CreateAccount), new CreateResult { Id = account.Id });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(CreateAccount), Message = e.Message });
            }
        }

        [Authorize]
        public Task GetAccount(Guid id)
        {
            try
            {
                var account = _chattle.AccountController.Get(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(GetAccount), new GetResult<Account> { Object = account });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(GetAccount), Message = e.Message });
            }
        }

        [Authorize]
        public Task GetMyAccount()
        {
            try
            {
                var account = _chattle.AccountController.Get(Context.User.GetId(), Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(GetMyAccount), new GetResult<Account> { Object = account });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(GetMyAccount), Message = e.Message });
            }
        }

        [Authorize]
        public Task DeleteAccount(Guid id)
        {
            try
            {
                _chattle.AccountController.Delete(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(DeleteAccount), new DeleteResult { Id = id });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(DeleteAccount), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetAccountIsActive(Guid id, bool isActive)
        {
            try
            {
                _chattle.AccountController.SetIsActive(id, isActive, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetAccountIsActive), new ManageResult<bool> { Id = id, NewValue = isActive });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetAccountIsActive), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetAccountGlobalPermission(Guid id, AccountGlobalPermission permission)
        {
            try
            {
                _chattle.AccountController.SetGlobalPermission(id, permission, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetAccountGlobalPermission), new ManageResult<AccountGlobalPermission> { Id = id, NewValue = permission });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetAccountGlobalPermission), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetAccountUsername(Guid id, string username)
        {
            try
            {
                _chattle.AccountController.SetUsername(id, username, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetAccountUsername), new ModifyResult<string> { Id = id, NewValue = username });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetAccountUsername), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetAccountPassword(Guid id, string password)
        {
            try
            {
                _chattle.AccountController.SetPassword(id, password, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetAccountPassword), new ModifyResult<string> { Id = id, NewValue = "N/A" });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetAccountPassword), Message = e.Message });
            }
        }
    }
}
