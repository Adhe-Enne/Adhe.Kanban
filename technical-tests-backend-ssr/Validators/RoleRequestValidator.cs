using FluentValidation;
using Kanban.Contracts.Dto.Request;

namespace technical_tests_backend_ssr.Validators
{
    public class RoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public RoleRequestValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "User" || role == "Admin")
                .WithMessage("Role must be either 'User' or 'Admin'.");
        }
    }
}
