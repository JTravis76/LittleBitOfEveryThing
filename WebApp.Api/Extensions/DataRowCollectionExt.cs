using System.Collections.ObjectModel;
using System.Data;
using WebApp.Api.Entities;

namespace WebApp.Api.Extensions
{
    public static class DataRowCollectionExt
    {
        private static T? ValueOrDefault<T>(this object value)
        {
            if (value is T)
                return (T)Convert.ChangeType(value, typeof(T));

            return default;
        }


        /// <summary>
        /// Map the data row to an entity of type User
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ICollection<User> MapToUser(this DataRowCollection collection)
        {
            var users = new Collection<User>();
            foreach (DataRow row in collection)
            {
                users.Add(new User 
                {
                    UserName = row["UserName"].ValueOrDefault<string>(),
                    FirstName = row["FirstName"].ValueOrDefault<string>(),
                    LastName = row["LastName"].ValueOrDefault<string>(),
                    Email = row["Email"].ValueOrDefault<string>(),
                    IsActive = row["IsActive"].ValueOrDefault<bool>()
                });
            }
            return users;
        }
    }
}
