using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Infrastructure.Services;

public class PdfService : IPdfService
{
    public byte[] GenerateToPdf<T>(IEnumerable<T> data, List<(string ColumnName, string PropertyName)> columns, string title)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                // Encabezado
                page.Header()
                    .AlignCenter()
                    .Text($"Reporte de {title}")
                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Darken2);

                // Contenido principal
                page.Content()
                    .Table(table =>
                    {
                        // Definir columnas
                        table.ColumnsDefinition(c =>
                        {
                            foreach (var _ in columns)
                                c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            foreach (var column in columns)
                            {
                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(6)
                                    .Text(column.ColumnName)
                                    .SemiBold().FontColor(Colors.Black);
                            }
                        });

                        // Filas de datos con estilo "zebra"
                        int rowIndex = 0;
                        foreach (var item in data)
                        {
                            bool isEvenRow = rowIndex % 2 == 0;
                            var bgColor = isEvenRow ? Colors.White : Colors.Grey.Lighten4;

                            foreach (var column in columns)
                            {
                                var propertyValue = typeof(T).GetProperty(column.PropertyName!)?.GetValue(item)?.ToString();
                                table.Cell()
                                    .Background(bgColor)
                                    .BorderBottom(1).BorderColor(Colors.Grey.Lighten3)
                                    .Padding(5)
                                    .Text(propertyValue ?? "");
                            }

                            rowIndex++;
                        }
                    });

                // Pie de página
                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
            });
        });

        return document.GeneratePdf();
    }
}
