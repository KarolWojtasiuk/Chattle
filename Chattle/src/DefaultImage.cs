using System;

namespace Chattle.Helpers
{
    public static class DefaultImage
    {
        private static readonly Uri baseUserImageUri = new Uri("https://api.adorable.io/avatars/512/");
        private static readonly Uri baseChannelImageUri = new Uri("https://api.adorable.io/avatars/512/");

        public static Uri GetUserImage(string username)
        {
            return new Uri(baseUserImageUri, username);
        }

        public static Uri GetChannelImage(string name)
        {
            return new Uri(baseChannelImageUri, name);
        }
    }
}
