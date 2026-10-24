using Microsoft.Data.Sqlite;
using System.Data.SqlClient;

namespace WebApp.Api.Extensions
{
    public static class DataReaderExt
    {
        public static T? ValueOrDefault<T>(this SqliteDataReader reader, string columnName)
        {
            if (reader is T)
                return (T)Convert.ChangeType(reader[columnName], typeof(T));

            return default;
        }
    }
}
