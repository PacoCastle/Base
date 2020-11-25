using Castle.Core.Internal;
using DatingApp.Api.Dtos;
using FluentValidation;

namespace DatingApp.Api.Validations
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