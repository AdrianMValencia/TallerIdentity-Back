using FluentValidation;

namespace TallerIdentity.Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName)
        .NotNull().WithMessage("El nombre no puede ser nulo.")
        .NotEmpty().WithMessage("El nombre no pueder ser vacío.");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("El apellido no puede ser nulo.")
            .NotEmpty().WithMessage("El apellido no pueder ser vacío.");
    }
}
