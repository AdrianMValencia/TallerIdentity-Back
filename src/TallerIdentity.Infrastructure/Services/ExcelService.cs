using ClosedXML.Excel;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Infrastructure.Services;

public class ExcelService : IExcelService
{
    public byte[] GenerateToExcel<T>(IEnumerable<T> data, List<(string ColumnName, string PropertyName)> columns)
    {
        var excelColumns = GetColumns(columns);
        var memoryStreamExcel = GenerateToExcel(data, excelColumns);
        var fileBytes = memoryStreamExcel.ToArray();
        return fileBytes;
    }

    public static List<TableColumn> GetColumns(
            IEnumerable<(string ColumnName, string PropertyName)> columnProperties)
    {
        // Crear una nueva instancia de la lista para almacenar las columnas
        var columns = new List<TableColumn>();

        // Recorremos cada tupla en la colección de columnas y propiedades
        foreach (var (ColumnName, PropertyName) in columnProperties)
        {
            // Crear una nueva instancia de TableColumn
            var column = new TableColumn()
            {
                // Establecer el nombre de la columna en base al valor de ColumnName de la tupla
                Label = ColumnName,
                // Establecer el nombre de la propiedad en base al valor de PropertyName de la tupla
                PropertyName = PropertyName
            };

            // Agregar la columna a la lista de columnas
            columns.Add(column);
        }

        // Devolver la lista de columnas generada
        return columns;
    }

    public MemoryStream GenerateToExcel<T>(IEnumerable<T> data, List<TableColumn> columns)
    {
        // Crear un nuevo objeto XLWorkbook para almacenar el libro de Excel
        var workbook = new XLWorkbook();

        // Agregar una nueva hoja de trabajo llamada "Listado"
        var worksheet = workbook.Worksheets.Add("Listado");

        // Escribir encabezados de columna
        for (int i = 0; i < columns.Count; i++)
        {
            // Establecer el valor del encabezado de columna en la celda correspondiente
            worksheet.Cell(1, i + 1).Value = columns[i].Label;
        }

        // Escribir datos de fila
        var rowIndex = 2;
        foreach (var item in data)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                // Obtener el valor de la propiedad correspondiente al nombre de propiedad especificado en TableColumn
                var propertyValue = typeof(T).GetProperty(columns[i].PropertyName!)?.GetValue(item)?.ToString();

                // Establecer el valor de la celda de la fila actual y la columna correspondiente
                worksheet.Cell(rowIndex, i + 1).Value = propertyValue;
            }
            rowIndex++;
        }

        // Guardar el libro de Excel en un MemoryStream
        var stream = new MemoryStream();
        workbook.SaveAs(stream);

        // Restablecer la posición del MemoryStream al principio para permitir su lectura
        stream.Position = 0;

        // Devolver el MemoryStream que contiene el libro de Excel generado
        return stream;
    }
}
