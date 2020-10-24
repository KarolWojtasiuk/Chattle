using System;

namespace Chattle
{
    public static class DefaultImage
    {
        private static readonly string baseUserImageUri = "https://avatars.dicebear.com/api/gridy/{userId}.svg?w=512&h=512&m=5&colorful=1&deterministic=1";
        private static readonly string baseServerImageUri = "https://avatars.dicebear.com/api/gridy/{serverId}.svg?w=512&h=512&m=5&colorful=1&deterministic=1";

        public static Uri GetUserImage(Guid userId)
        {
            return new Uri(baseUserImageUri.Replace("{userId}", userId.ToString()));
        }

        public static Uri GetServerImage(Guid serverId)
        {
            return new Uri(baseServerImageUri.Replace("{serverId}", serverId.ToString()));
        }
    }
}
