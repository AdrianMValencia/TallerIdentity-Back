namespace TallerIdentity.Application.Interfaces.Services;

public  interface IExcelService
{
    byte[] GenerateToExcel<T>(IEnumerable<T> data, List<(string ColumnName, string PropertyName)> columns);
}
