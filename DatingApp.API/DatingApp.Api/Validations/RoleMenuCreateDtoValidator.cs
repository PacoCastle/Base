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
    public class RoleMenuCreateDtoValidator : AbstractValidator<RoleMenuCreateDto>
    {        
        public RoleMenuCreateDtoValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("El id no puede ir vacio");     
        }
    }
}