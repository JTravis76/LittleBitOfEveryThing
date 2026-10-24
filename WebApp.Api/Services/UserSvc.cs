using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using WebApp.Api.Entities;
using WebApp.Api.Extensions;

namespace WebApp.Api.Services
{
    public class UserSvc
    {
        private readonly IConfiguration _configuration;
        private readonly string _conStr = String.Empty;

        public UserSvc(IConfiguration configuration)
        {
            _configuration = configuration;
            _conStr = configuration.GetConnectionString("");
        }


        /// <summary>
        /// Gets all users via the SQL Data Reader
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<User>> GetUsersAsync()
        {
            using (var con = new SqliteConnection(_conStr))
            {
                con.Open();

                ICollection<User> users = new Collection<User>();

                string query = "SELECT * FROM [Users]";
                using (var cmd = new SqliteCommand(query, con))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserName = reader.ValueOrDefault<string>("UserName"),
                                FirstName = reader.ValueOrDefault<string>("FirstName"),
                                LastName = reader.ValueOrDefault<string>("LastName"),
                                Email = reader.ValueOrDefault<string>("Email"),
                                IsActive = reader.ValueOrDefault<bool>("IsActive"),
                            });
                        }
                    }
                }

                con.Close();
                return users;
            }
        }

        /// <summary>
        /// Gets all users via the SQL Data Adapter
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<User>> GetUsersAsync2()
        {
            ICollection<User> users = new Collection<User>();

            using (var con = new SqlConnection(_conStr))
            {
                con.Open();

                string query = "SELECT * FROM [Users]";
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;

                        var dt = new DataTable("Results");
                        adapter.Fill(dt);

                        // Map results to entity
                        users = dt.Rows.MapToUser();
                    }
                }
                con.Close();
            }

            //using (var con = new SqliteConnection(_conStr))
            //{
            //    con.Open();

            //    string query = "SELECT * FROM [Users]";
            //    using (var cmd = new SqliteCommand(query, con))
            //    {
            //        using (var adapter = new SqlDataAdapter())
            //        {
            //            adapter.SelectCommand = new SqliteCommand(query, con);

            //            var dt = new DataTable("Results");
            //            adapter.Fill(dt);

            //            // Map results to entity
            //            users = dt.Rows.MapToUser();
            //        }
            //    }
            //    await con.CloseAsync();
            //}
            return users;
        }


    }
}
