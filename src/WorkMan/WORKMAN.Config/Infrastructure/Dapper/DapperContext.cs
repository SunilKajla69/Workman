using Npgsql;
using System.Data;

namespace WORKMAN.Config.Infrastructure.Dapper
{
    /// <summary>
    /// Implementation of Dapper database context for PostgreSQL
    /// </summary>
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("configDatabase") 
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'configDatabase' not found");
        }

        /// <summary>
        /// Creates and returns a new PostgreSQL database connection
        /// </summary>
        /// <returns>NpgsqlConnection instance</returns>
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
