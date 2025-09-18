namespace technical_tests_backend_ssr.Dto.Response
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; }
    }
}
