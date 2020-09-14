using System;
using System.Net;
using System.Linq;

namespace Chattle
{
    public static class ModelVerifier
    {
        public static void ThrowDuplicateException(IIdentifiable item)
        {
            throw new ModelVerificationException(item, "Object with the same id already exists.");
        }

        public static void ThrowDoesNotExistsException()
        {
            throw new ModelVerificationException("Object does not exists in the database.");
        }

        public static void AnotherUserAssignedException(Account account)
        {
            throw new ModelVerificationException(account, "There is already a user assigned to this account.");
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
            if (server.Roles.First(r => r.Id == Guid.Empty) == null)
            {
                throw new ModelVerificationException(server, "Default role does not exists.");
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
}
