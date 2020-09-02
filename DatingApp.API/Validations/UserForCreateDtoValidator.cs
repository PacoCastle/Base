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
    public class UserForCreateDtoValidator : AbstractValidator<UserForCreateDto>
    {        
        public UserForCreateDtoValidator()
        {
            RuleFor(m => m.UserName)
                .NotEmpty()
                .WithMessage("'UserName' no puede ser vac�o.");
            RuleFor(m => m.Password)
                .NotEmpty()
                .WithMessage("'Password'  no puede ser vac�o.");
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("'Name'  no puede ser vac�o.");
            RuleFor(m => m.LastName)
                .NotEmpty()
                .WithMessage("'LastName'  no puede ser vac�o.");
            RuleFor(m => m.Email)
                .NotEmpty()
                .WithMessage("'Email'  no puede ser vac�o.");
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithMessage("'Email'  Es requerido un Email valido.");
            RuleForEach(m => m.Roles).NotEmpty().WithMessage("Roles {CollectionIndex} no puede ser vac�o.");
        }
    }
}