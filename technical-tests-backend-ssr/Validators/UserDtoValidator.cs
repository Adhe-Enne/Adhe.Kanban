namespace technical_tests_backend_ssr.Validators
{
    using FluentValidation;
    using technical_tests_backend_ssr.Dto.Request;

    public class UserDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(26);

            RuleFor(x => x.Country)
                .MaximumLength(30);

            RuleFor(x => x.City)
                .MaximumLength(30);
        }
    }
}
