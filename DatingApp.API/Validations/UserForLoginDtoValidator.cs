using DatingApp.API.Dtos;
using FluentValidation;

namespace DatingApp.API.Validations
{
    /*
    The main UserForLoginDtoValidator class
    Contains all validations rules for Login User 
    */
    /// <summary>
    /// The UserForLoginDtoValidator class.
    /// Contains all validations rules for Create User
    /// </summary>    
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {        
        public UserForLoginDtoValidator()
        {
            RuleFor(m => m.UserName)
                .NotEmpty()
                .WithMessage("'UserName'  no puede ser vac�o.");
            RuleFor(m => m.Password)
                .NotEmpty()
                .WithMessage("'Password'  no puede ser vac�o.");
        }
    }
}