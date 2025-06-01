
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TallerIdentity.Application.Commons.Bases;
using ValidationException = TallerIdentity.Application.Commons.Exceptions.ValidationException;

namespace TallerIdentity.Application.Behaviours;

public class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(T request, CancellationToken cancellationToken = default)
    {
        var validators = _serviceProvider.GetServices<IValidator<T>>();

        if (!validators.Any()) return;

        var context = new ValidationContext<T>(request);

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .Select(err => new BaseError
            {
                PropertyName = err.PropertyName,
                ErrorMessage = err.ErrorMessage
            }).ToList();

        if (failures.Any())
            throw new ValidationException(failures);
    }
}
