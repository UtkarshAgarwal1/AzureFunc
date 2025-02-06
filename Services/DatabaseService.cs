using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Company.Function.Models;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Company.Function.Services
{
    public class DatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private const string ConnectionString = "Data Source=localdata.db";
        

        public DatabaseService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DatabaseService>();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var tableCommand = @"
                CREATE TABLE IF NOT EXISTS Posts (
                    Id INTEGER PRIMARY KEY,
                    UserId INTEGER,
                    Title TEXT,
                    Body TEXT
                )";

            var command = connection.CreateCommand();
            command.CommandText = tableCommand;
            command.ExecuteNonQuery();
            _logger.LogInformation("Database initialized successfully.");
            }
            catch (SqliteException ex)
            {
                _logger.LogError($"Database initialization error: {ex.Message}");
                throw;
            }
        }
        

        public List<PostModel> ProcessPosts(List<PostModel> posts)
        {
            return posts.Where(post => post.UserId == 1)
                        .Select(post =>
                        {
                            post.Title = post.Title.ToUpper();
                            return post;
                        }).ToList();
        }

        public void StoreData(List<PostModel> posts)
        {
          try
            {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            foreach (var post in posts)
            {
                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT COUNT(1) FROM Posts WHERE Id = @Id";
                selectCommand.Parameters.AddWithValue("@Id", post.Id);

                var exists = (selectCommand.ExecuteScalar() as long? ?? 0) > 0;
                if (exists) continue;

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO Posts (Id, UserId, Title, Body) 
                    VALUES (@Id, @UserId, @Title, @Body)";
                insertCommand.Parameters.AddWithValue("@Id", post.Id);
                insertCommand.Parameters.AddWithValue("@UserId", post.UserId);
                insertCommand.Parameters.AddWithValue("@Title", post.Title);
                insertCommand.Parameters.AddWithValue("@Body", post.Body);

                insertCommand.ExecuteNonQuery();
            }
            _logger.LogInformation("Data successfully stored in the database.");
            }
            catch (SqliteException ex)
            {
                _logger.LogError($"Database connection issue: {ex.Message}");
            }
             catch (System.Exception ex)
            {
                _logger.LogError($"Unexpected error during data storage: {ex.Message}");
            }
        }
    }
}
