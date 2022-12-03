namespace GameLibrary.Core.Models.Admin
{
    public class UserServiceModel
    {
        public string UserId { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? PhoneNumber { get; init; } = null;
    }
}
