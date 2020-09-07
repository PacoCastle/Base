using DatingApp.API.Dtos;
using FluentValidation;

namespace DatingApp.API.Validations
{
    /*
    The main UserForCreateDtoValidator class
    Contains all validations rules for Create User 
    */
    /// <summary>
    /// The UserForCreateDtoValidator class.
    /// Contains all validations rules for Create User
    /// </summary>    
    public class UserForUpdateDtoValidator : AbstractValidator<UserForUpdateDto>
    {        
        public UserForUpdateDtoValidator()
        {
            RuleFor(m => m.Status)
                .NotEmpty()
                .WithMessage("'Status'  no puede ser vacío.");
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithMessage("'Email'  Es requerido un Email valido.");
            RuleForEach(m => m.RoleNames).NotEmpty().WithMessage("Roles {CollectionIndex} no puede ser vacío.");
        }
    }
}