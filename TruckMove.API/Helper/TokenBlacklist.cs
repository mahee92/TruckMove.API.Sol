namespace TruckMove.API.Helper
{
    public static class TokenBlacklist
    {
        private static readonly HashSet<string> BlacklistedTokens = new HashSet<string>();

        public static void AddToken(string token)
        {
            BlacklistedTokens.Add(token);
        }

        public static bool IsTokenBlacklisted(string token)
        {
            return BlacklistedTokens.Contains(token);
        }
    }
}
