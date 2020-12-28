using DatingApp.Api.Dtos;
using FluentValidation;

namespace DatingApp.Api.Validations
{
    /*
    The main MachPartAttemUpdateDtoValidator class
    Contains all validations rules for Create MachinePartAttepmt 
    */
    /// <summary>
    /// The MachPartAttemCreateDtoValidator class.
    /// Contains all validations rules for Update  MachinePartAttepmt
    /// </summary>    
    public class MachPartAttemUpdateDtoValidator : AbstractValidator<MachPartAttemUpdateDto>
    {        
        public MachPartAttemUpdateDtoValidator()
        {
            RuleFor(m => m.AvailableAttempts)
                .NotEmpty()
                .WithMessage("'AvailableAttempts' must not be empty.");                 
        }
    }
}