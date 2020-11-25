using DatingApp.Api.Dtos;
using FluentValidation;

namespace DatingApp.Api.Validations
{
    /*
    The main Math class
    Contains all properties for Create MachinePartAttepmt register
    */
    /// <summary>
    /// The MachPartAttemCreateDtoValidator class.
    /// Contains all validations rules for Create MachinePartAttepmt Create
    /// </summary>    
    public class MachPartAttemCreateDtoValidator : AbstractValidator<MachPartAttemCreateDto>
    {        
        public MachPartAttemCreateDtoValidator()
        {
            RuleFor(m => m.MachineModel)
                .NotEmpty()
                .WithMessage("'MachineModel' must not be empty.");     

            RuleFor(m => m.PartModel)
                .NotEmpty()
                .WithMessage("'PartModel' must not be empty.");  

            RuleFor(a => a.InternalSequence)
                .NotEmpty()
                .WithMessage("'InternalSequence' must not be empty.");              
        }
    }
}