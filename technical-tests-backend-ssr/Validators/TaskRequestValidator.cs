using FluentValidation;
using Kanban.Contracts.Dto.Request;
using TaskStatus = Kanban.Contracts.Enums.TaskStatus;
using TaskPriority = Kanban.Contracts.Enums.TaskPriority;

namespace technical_tests_backend_ssr.Validators
{
    public class TaskRequestValidator : AbstractValidator<TaskRequest>
    {
        public TaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título de la tarea es requerido.")
                .MaximumLength(200).WithMessage("El título de la tarea no debe exceder los 200 caracteres.")
                .MinimumLength(3).WithMessage("El título de la tarea debe tener al menos 3 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("El título de la tarea solo debe contener caracteres alfanuméricos y espacios.")
                .Must(title => !string.IsNullOrWhiteSpace(title) && title.Trim().Length > 0).WithMessage("El título de la tarea no debe ser solo espacios en blanco.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción de la tarea es requerida.")
                .MaximumLength(1000).WithMessage("La descripción de la tarea no debe exceder los 1000 caracteres.")
                .MinimumLength(5).WithMessage("La descripción de la tarea debe tener al menos 5 caracteres.")
                .Must(desc => !string.IsNullOrWhiteSpace(desc) && desc.Trim().Length > 0).WithMessage("La descripción de la tarea no debe ser solo espacios en blanco.")
                .Matches(@"^[a-zA-Z0-9\s.,!?'-]+$").WithMessage("La descripción de la tarea contiene caracteres inválidos.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("La prioridad debe ser válida.")
                .NotEmpty().WithMessage("La prioridad es requerida.")
                .Must(p => p == TaskPriority.Low || p == TaskPriority.Medium || p == TaskPriority.High)
                .WithMessage("La prioridad debe ser Low, Medium o High.");


            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado debe ser válido.")
                .NotEmpty().WithMessage("El estado es requerido.")
                .Must(s => s == TaskStatus.ToDo || s == TaskStatus.InProgress || s == TaskStatus.Done).WithMessage("El estado debe ser ToDo, InProgress o Done.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de vencimiento debe ser una fecha futura.")
                .NotEmpty().WithMessage("La fecha de vencimiento es requerida.")
                .Must(date => date != default(DateTime)).WithMessage("La fecha de vencimiento no es válida.");

            RuleFor(x => x.BoardId)
                .NotEmpty().WithMessage("La referencia a un Tablero es requerida.")
                .Must(id => id != Guid.Empty).WithMessage("La referencia a un Tablero no es válida.")
                .GreaterThan(Guid.Empty).WithMessage("La referencia a un Tablero no es válida.")
                .LessThan(Guid.NewGuid()).WithMessage("La referencia a un Tablero no es válida.")
                .NotEqual(Guid.Empty).WithMessage("La referencia a un Tablero no es válida.");

            RuleFor(x => x.AssignedUserId)
                .NotEmpty().WithMessage("La asignacion de un Usuario es requerida.")
                .Must(id => id != Guid.Empty).WithMessage("La referencia a un Usuario no es válida.")
                .GreaterThan(Guid.Empty).WithMessage("La referencia a un Usuario no es válida.")
                .LessThan(Guid.NewGuid()).WithMessage("La referencia a un Usuario no es válida.")
                .NotEqual(Guid.Empty).WithMessage("La referencia a un Usuario no es válida.");

            RuleFor(x => x.ColumnId)
                .NotEmpty().WithMessage("La referencia a una Columna es requerida.")
                .Must(id => id != Guid.Empty).WithMessage("La referencia a una Columna no es válida.")
                .GreaterThan(Guid.Empty).WithMessage("La referencia a una Columna no es válida.")
                .LessThan(Guid.NewGuid()).WithMessage("La referencia a una Columna no es válida.")
                .NotEqual(Guid.Empty).WithMessage("La referencia a una Columna no es válida.");
        }
    }
}
