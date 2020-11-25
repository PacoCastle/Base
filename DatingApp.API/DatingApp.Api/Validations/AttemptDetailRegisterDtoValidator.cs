using DatingApp.Api.Dtos;
using FluentValidation;

namespace DatingApp.Api.Validations
{
    public class AttemptDetailRegisterDtoValidator : AbstractValidator<AttemptDetailRegisterDto>
    {        
        public AttemptDetailRegisterDtoValidator()
        {
            RuleFor(a => a.MachinePartAttemptId)
                .NotEmpty()
                .WithMessage("'MachinePartAttemptId' must not be 0.");

            RuleFor(m => m.AnguloLH)
                .NotEmpty()
                .WithMessage("'AnguloLH' must not be 0.")
                .MaximumLength(3)
                .WithMessage("'AnguloLH' must not be greater than 3");
        }
    }
}