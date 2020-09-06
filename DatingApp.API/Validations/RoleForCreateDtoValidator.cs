using Castle.Core.Internal;
using DatingApp.API.Dtos;
using FluentValidation;

namespace DatingApp.API.Validations
{
    public class RoleForCreateDtoValidator : AbstractValidator<RoleForCreateDto>
    {        
        public RoleForCreateDtoValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("El nombre del Rol no puede ir vacio");
        }
    }
}