namespace ARK.WebApi.Application.Identity.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);