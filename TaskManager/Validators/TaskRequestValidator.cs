using FluentValidation;

namespace TaskManager.Validators
{
    public class TaskRequestValidator : AbstractValidator<TaskRequest>
    {
        public TaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Priority)
                .Must(p => new[] { "Low", "Medium", "High" }.Contains(p))
                .When(x => !string.IsNullOrEmpty(x.Priority))
                .WithMessage("Priority must be Low, Medium, or High");

            RuleFor(x => x.DueDate)
                .Must(date => date == null || date > DateTime.Now)
                .WithMessage("Due date must be in the future");
        }
    }
}
