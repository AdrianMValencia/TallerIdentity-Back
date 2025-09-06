namespace TallerIdentity.Application.Helpers;

public class ReportColumns
{
    public static List<(string ColumnName, string PropertyName)> GetColumnsUsers()
    {
        // Crear una nueva lista de tuplas que almacenará las columnas y propiedades correspondientes
        var columnProperties = new List<(string ColumnName, string PropertyName)>
        {
            // Definimos una tupla con el nombre de columna y la propiedad
            ("NOMBRES", "FirstName"),
            ("APELLIDOS", "LastName"),
            ("CORREO", "Email"),
            ("TELÉFONO", "Phone"),
            ("DIRECCIÓN", "Address"),
            ("ESTADO", "StateDescription")
        };

        // Devolver la lista de tuplas que contiene las columnas y propiedades
        return columnProperties;
    }

}
