namespace WebApi_Shwetha.Controllers
{
    public class RefreshToken
    {
       
            public string Token { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
            public DateTime Created { get; set; }
            public DateTime Expires { get; set; }
            public DateTime? Revoked { get; set; }

            public bool IsExpired => DateTime.UtcNow >= Expires;
            public bool IsActive => Revoked == null && !IsExpired;
        
    }
}
