namespace ARK.WebApi.Application.Identity.Tokens;

public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);