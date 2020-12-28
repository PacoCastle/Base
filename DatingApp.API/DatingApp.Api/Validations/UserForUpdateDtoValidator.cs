using DatingApp.Api.Dtos;
using FluentValidation;

namespace DatingApp.Api.Validations
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
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithMessage("'Email'  Es requerido un Email valido.");
            RuleForEach(m => m.RoleNames).NotEmpty().WithMessage("Roles {CollectionIndex} no puede ser vacío.");
        }
    }
}