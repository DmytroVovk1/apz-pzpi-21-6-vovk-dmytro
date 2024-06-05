namespace Discerniy.Domain.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public TokenResponse(string token, DateTime expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }
    }
}
