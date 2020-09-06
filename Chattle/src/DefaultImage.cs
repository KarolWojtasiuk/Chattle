using System;

namespace Chattle
{
    public static class DefaultImage
    {
        private static readonly Uri baseUserImageUri = new Uri("https://api.adorable.io/avatars/512/User_");
        private static readonly Uri baseServerImageUri = new Uri("https://api.adorable.io/avatars/512/Server_");

        public static Uri GetUserImage(Guid userId)
        {
            return new Uri(baseUserImageUri, userId.ToString());
        }

        public static Uri GetServerImage(Guid serverId)
        {
            return new Uri(baseServerImageUri, serverId.ToString());
        }
    }
}
