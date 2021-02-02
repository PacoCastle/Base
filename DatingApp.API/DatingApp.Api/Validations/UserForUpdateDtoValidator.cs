using DatingApp.Api.Dtos;
using FluentValidation;
using System.Text.RegularExpressions;

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
        const string expression = @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)";

        readonly Regex regEx = new Regex(expression, RegexOptions.IgnoreCase);
        public UserForUpdateDtoValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithMessage("'Email'  Es requerido un Email valido.");
            RuleForEach(m => m.RoleNames).NotEmpty().WithMessage("Roles {CollectionIndex} no puede ser vac�o.");

            When(m => m.Password.Length > 0, () => {
                RuleFor(m => m.Password)
                .Matches(regEx)
                .WithMessage("Password should contain at least 1 special character, 1 upper letter, 1 lower letter and 1 number");
            });
        }
    }
}