using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        #region Account
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
        #endregion

        #region User
        [Authorize]
        public Task CreateUser(string nickname, UserType userType)
        {
            try
            {
                var user = new User(nickname, Context.User.GetId(), userType);
                _chattle.UserController.Create(user, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(CreateUser), new CreateResult { Id = user.Id });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(CreateUser), Message = e.Message });
            }
        }

        [Authorize]
        public Task GetUser(Guid id)
        {
            try
            {
                var user = _chattle.UserController.Get(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(GetUser), new GetResult<User> { Object = user });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(GetUser), Message = e.Message });
            }
        }

        [Authorize]
        public Task DeleteUser(Guid id)
        {
            try
            {
                _chattle.UserController.Delete(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(DeleteUser), new DeleteResult { Id = id });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(DeleteUser), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetUserIsActive(Guid id, bool isActive)
        {
            try
            {
                _chattle.UserController.SetIsActive(id, isActive, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetUserIsActive), new ManageResult<bool> { Id = id, NewValue = isActive });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetUserIsActive), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetUserGlobalPermission(Guid id, UserGlobalPermission permission)
        {
            try
            {
                _chattle.UserController.SetGlobalPermission(id, permission, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetUserGlobalPermission), new ManageResult<UserGlobalPermission> { Id = id, NewValue = permission });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetUserGlobalPermission), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetUserNickname(Guid id, string nickname)
        {
            try
            {
                _chattle.UserController.SetNickname(id, nickname, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetUserNickname), new ModifyResult<string> { Id = id, NewValue = nickname });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetUserNickname), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetUserImage(Guid id, Uri image)
        {
            try
            {
                _chattle.UserController.SetImage(id, image, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetUserImage), new ModifyResult<Uri> { Id = id, NewValue = image });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetUserImage), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetUserDefaultImage(Guid id)
        {
            try
            {
                _chattle.UserController.SetDefaultImage(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetUserDefaultImage), new ModifyResult<Uri> { Id = id, NewValue = DefaultImage.GetUserImage(id) });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetUserDefaultImage), Message = e.Message });
            }
        }
        #endregion

        #region Server
        [Authorize]
        public Task CreateServer(string name, string description, string image)
        {
            try
            {
                Server server = null;
                if (String.IsNullOrWhiteSpace(description) && String.IsNullOrWhiteSpace(image))
                {
                    server = new Server(name, Context.User.GetId());
                }
                else if (String.IsNullOrWhiteSpace(image))
                {
                    server = new Server(name, Context.User.GetId(), description);
                }
                else
                {
                    server = new Server(name, Context.User.GetId(), description, new Uri(image));
                }

                _chattle.ServerController.Create(server, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(CreateServer), new CreateResult { Id = server.Id });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(CreateServer), Message = e.Message });
            }
        }

        [Authorize]
        public Task GetServer(Guid id)
        {
            try
            {
                var server = _chattle.ServerController.Get(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(GetServer), new GetResult<Server> { Object = server });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(GetServer), Message = e.Message });
            }
        }

        [Authorize]
        public Task DeleteServer(Guid id)
        {
            try
            {
                _chattle.ServerController.Delete(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(DeleteServer), new DeleteResult { Id = id });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(DeleteServer), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetServerName(Guid id, string name)
        {
            try
            {
                _chattle.ServerController.SetName(id, name, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetServerName), new ModifyResult<string> { Id = id, NewValue = name });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetServerName), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetServerDescription(Guid id, string description)
        {
            try
            {
                _chattle.ServerController.SetDescription(id, description, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetServerDescription), new ModifyResult<string> { Id = id, NewValue = description });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetServerDescription), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetServerImage(Guid id, string image)
        {
            try
            {
                var imageUri = new Uri(image);
                _chattle.ServerController.SetImage(id, imageUri, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetServerImage), new ModifyResult<Uri> { Id = id, NewValue = imageUri });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetServerImage), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetServerDefaultImage(Guid id)
        {
            try
            {
                _chattle.ServerController.SetDefaultImage(id, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetServerDefaultImage), new ModifyResult<Uri> { Id = id, NewValue = DefaultImage.GetServerImage(id) });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetServerDefaultImage), Message = e.Message });
            }
        }

        [Authorize]
        public Task SetServerRoles(Guid id, List<Role> roles)
        {
            try
            {
                _chattle.ServerController.SetRoles(id, roles, Context.User.GetId());
                return Clients.Caller.SendAsync(nameof(SetServerRoles), new ModifyResult<List<Role>> { Id = id, NewValue = roles });
            }
            catch (Exception e)
            {
                return Clients.Caller.SendAsync("Error", new ErrorResult { Method = nameof(SetServerRoles), Message = e.Message });
            }
        }
        #endregion

        #region Channel

        #endregion

        #region Message

        #endregion
    }
}
