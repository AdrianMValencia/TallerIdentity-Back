namespace TallerIdentity.Application.Interfaces.Services;

public interface IPdfService
{
    byte[] GenerateToPdf<T>(IEnumerable<T> data, List<(string ColumnName, string PropertyName)> columns, string title);
}
