using Dapper;
using Microsoft.Data.SqlClient;

namespace GroceryAppAPI.Repository
{
    // Generic base repository class for common database operations using Dapper
    public class BaseRepository<T>
    {
        private readonly string _connectionString;

        // Constructor to initialize the repository with a database connection string
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all records based on the provided SQL query and optional parameters
        public IEnumerable<T> GetAll(string query, object? parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Execute the query and return the result
                return connection.Query<T>(query, parameters);
            }
        }

        // Get a single record based on the provided SQL query and parameters
        public T Get(string query, object parameters)
        {
            // Retrieve all records and return the first one (or default if none)
            return GetAll(query, parameters).FirstOrDefault();
        }

        // Add a new record to the database based on the provided SQL query and parameters
        public int Add(string query, object parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Execute the query and return the generated ID (if applicable)
                return connection.QueryFirstOrDefault<int>(query, parameters);
            }
        }

        // Update records in the database based on the provided SQL query and parameters
        public void Update(string query, object parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Execute the update query
                connection.Execute(query, parameters);
            }
        }

        // Delete records from the database based on the provided SQL query and parameters
        public void Delete(string query, object parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Execute the delete query
                connection.Execute(query, parameters);
            }
        }
    }
}
