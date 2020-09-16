using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace Chattle
{
    public static class ModelVerifier
    {
        public static void CheckPermission(Guid callerId, List<Role> roles, Permission permission)
        {
            foreach (var role in roles)
            {
                if (role.Users.Contains(callerId))
                {
                    if (role.Permission.HasFlag(permission))
                    {
                        return;
                    }
                }
            }

            throw new ModelVerificationException("User has insufficient permissions.");
        }

        #region Account
        public static void VerifyAccount(Account account, string password)
        {
            VerifyAccountUsername(account);
            VerifyAccountPassword(account, password);
        }

        public static void VerifyAccount(Account account)
        {
            VerifyAccountUsername(account);
        }

        public static void VerifyAccountUsername(Account account)
        {
            if (account.Username.Length < 5)
            {
                throw new ModelVerificationException(account, "Username should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(account.Username))
            {
                throw new ModelVerificationException(account, "Username should not be empty or contain only whitespace.");
            }
        }

        public static void VerifyAccountPassword(Account account, string password)
        {
            if (password.Length < 5)
            {
                throw new ModelVerificationException(account, "Password should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(password))
            {
                throw new ModelVerificationException(account, "Password should not be empty or contain only whitespace.");
            }
        }
        #endregion

        #region User
        public static void VerifyUser(User user)
        {
            VerifyUserNickname(user);
            VerifyUserImage(user);
        }

        public static void VerifyUserNickname(User user)
        {
            if (user.Nickname.Length < 5)
            {
                throw new ModelVerificationException(user, "Nickname should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(user.Nickname))
            {
                throw new ModelVerificationException(user, "Nickname should not be empty or contain only whitespace.");
            }
        }

        public static void VerifyUserImage(User user)
        {
            if (!WebRequest.Create(user.Image).GetResponse().ContentType.ToLower().StartsWith("image/"))
            {
                throw new ModelVerificationException(user, "Image Uri should be of type `image/*`.");
            }
        }
        #endregion

        #region Server
        public static void VerifyServer(Server server, User author)
        {
            VerifyServerName(server);
            VerifyServerImage(server);
            VerifyServerRoles(server);
            VerifyServerAuthor(author);
        }

        public static void VerifyServer(Server server)
        {
            VerifyServerName(server);
            VerifyServerImage(server);
            VerifyServerRoles(server);
        }

        public static void VerifyServerName(Server server)
        {
            if (server.Name.Length < 5)
            {
                throw new ModelVerificationException(server, "Name should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(server.Name))
            {
                throw new ModelVerificationException(server, "Name should not be empty or contain only whitespace.");
            }
        }

        public static void VerifyServerImage(Server server)
        {
            if (!WebRequest.Create(server.Image).GetResponse().ContentType.ToLower().StartsWith("image/"))
            {
                throw new ModelVerificationException(server, "Image Uri should be of type `image/*`.");
            }
        }

        public static void VerifyServerRoles(Server server)
        {
            var defaultRole = server.Roles.FirstOrDefault(r => r.Id == Guid.Empty);

            if (defaultRole == null)
            {
                throw new ModelVerificationException(server, "Default role does not exists.");
            }

            foreach (var role in server.Roles)
            {
                foreach (var user in role.Users)
                {
                    if (!defaultRole.Users.Contains(user))
                    {
                        throw new ModelVerificationException(server, "Assign a primary role to a user before assigning other roles to them.");
                    }
                }
            }
        }

        public static void VerifyServerAuthor(User author)
        {
            if (author == null)
            {
                throw new DoesNotExistsException();
            }
        }
        #endregion

        #region Channel
        public static void VerifyChannel(Channel channel)
        {
            VerifyChannelName(channel);
        }

        public static void VerifyChannelName(Channel channel)
        {
            if (channel.Name.Length < 5)
            {
                throw new ModelVerificationException(channel, "Name should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(channel.Name))
            {
                throw new ModelVerificationException(channel, "Name should not be empty or contain only whitespace.");
            }
        }
        #endregion
    }

    public class ModelVerificationException : Exception
    {
        public ModelVerificationException() { }

        public ModelVerificationException(string message) : base(message) { }
        public ModelVerificationException(IIdentifiable item, string message) : base($"{item.GetType().Name}({item.Id}) -> {message}") { }
    }

    public class DoesNotExistsException : ModelVerificationException
    {
        public DoesNotExistsException() : base("Object does not exists in the database.") { }
    }

    public class NotOwnerException : ModelVerificationException
    {
        public NotOwnerException(IIdentifiable item) : base(item, "Only owner can perform server-based actions.") { }
    }

    public class DuplicateException : ModelVerificationException
    {
        public DuplicateException(IIdentifiable item) : base(item, "Object with the same id already exists.") { }
    }

    public class AnotherUserAssigned : ModelVerificationException
    {
        public AnotherUserAssigned(IIdentifiable item) : base(item, "There is already a user assigned to this account.") { }
    }
}