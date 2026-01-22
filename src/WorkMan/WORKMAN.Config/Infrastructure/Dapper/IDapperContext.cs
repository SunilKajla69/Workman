using System.Data;

namespace WORKMAN.Config.Infrastructure.Dapper
{
    /// <summary>
    /// Interface for Dapper database context
    /// </summary>
    public interface IDapperContext
    {
        /// <summary>
        /// Creates and returns a new database connection
        /// </summary>
        /// <returns>Database connection</returns>
        IDbConnection CreateConnection();
    }
}
