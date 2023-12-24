using System.Reflection;

public static class DatabaseUtil
{
    public static string GetTableName<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);

        // Access public static fields
        var fieldInfo = type.GetField("TableName", BindingFlags.Public | BindingFlags.Static);
        if (fieldInfo != null)
        {
            return fieldInfo.GetValue(null)?.ToString() ?? string.Empty;
        }

        // This part is not necessary for constants but might be useful for properties
        var propertyInfo = type.GetProperty("TableName", BindingFlags.Public | BindingFlags.Static);
        if (propertyInfo != null)
        {
            return propertyInfo.GetValue(null)?.ToString() ?? string.Empty;
        }

        return type.Name; // Fallback to type name
    }
}
