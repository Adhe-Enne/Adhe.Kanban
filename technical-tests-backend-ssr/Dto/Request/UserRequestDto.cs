namespace technical_tests_backend_ssr.Dto.Request
{
    public class UserRequestDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
