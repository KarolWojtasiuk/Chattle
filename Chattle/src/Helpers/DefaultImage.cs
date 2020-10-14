using System;

namespace Chattle
{
    public static class DefaultImage
    {
        //! Find working API;
        // private static readonly Uri baseUserImageUri = new Uri("https://api.adorable.io/avatars/512/User_");
        // private static readonly Uri baseServerImageUri = new Uri("https://api.adorable.io/avatars/512/Server_");

        public static Uri GetUserImage(Guid userId)
        {
            // return new Uri(baseUserImageUri, userId.ToString());
            return new Uri("http://placehold.jp/500x500.jpg");
        }

        public static Uri GetServerImage(Guid serverId)
        {
            return new Uri("http://placehold.jp/500x500.jpg");
            // return new Uri(baseServerImageUri, serverId.ToString());
        }
    }
}
