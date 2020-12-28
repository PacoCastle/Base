using Castle.Core.Internal;
using DatingApp.Api.Dtos;
using FluentValidation;

namespace DatingApp.Api.Validations
{
    public class RoleForUpdateDtoValidator : AbstractValidator<RoleForUpdateDto>
    {        
        public RoleForUpdateDtoValidator()
        {
            RuleForEach(m => m.Menus).NotEmpty().WithMessage("Menus {CollectionIndex} no puede ser vacío.");
        }
    }
}