using System;

namespace Chattle
{
    public static class ModelVerifier
    {
        public static void VerifyAccount(Account account)
        {
            //Username
            if (account.Username.Length < 5)
            {
                throw new ModelVerificationException(account, "Username should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(account.Username))
            {
                throw new ModelVerificationException(account, "Username should not be empty or contain only whitespace.");
            }
        }

        public static void VerifyAccount(Account account, string password)
        {
            //Username
            VerifyAccount(account);

            //Password
            if (password.Length < 5)
            {
                throw new ModelVerificationException(account, "Password should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(password))
            {
                throw new ModelVerificationException(account, "Password should not be empty or contain only whitespace.");
            }
        }
    }

    public class ModelVerificationException : Exception
    {
        public ModelVerificationException() { }

        public ModelVerificationException(IIdentifiable item, string message) : base($"{item.GetType().Name}({item.Id}) -> {message}") { }
    }
}
