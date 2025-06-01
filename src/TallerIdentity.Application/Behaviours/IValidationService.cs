namespace TallerIdentity.Application.Behaviours;

public interface IValidationService
{
    Task ValidateAsync<T>(T request, CancellationToken cancellationToken = default);
}
