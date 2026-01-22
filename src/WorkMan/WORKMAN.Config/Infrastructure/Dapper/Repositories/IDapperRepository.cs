using System.Data;

namespace WORKMAN.Config.Infrastructure.Dapper.Repositories
{
    /// <summary>
    /// Base interface for repositories that execute stored procedures using Dapper
    /// </summary>
    public interface IDapperRepository
    {
        /// <summary>
        /// Executes a stored procedure and returns a single result
        /// </summary>
        Task<T?> ExecuteStoredProcedureSingleAsync<T>(
            string storedProcedureName, 
            object? parameters = null, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure and returns multiple results
        /// </summary>
        Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(
            string storedProcedureName, 
            object? parameters = null, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure without returning results
        /// </summary>
        Task<int> ExecuteStoredProcedureNonQueryAsync(
            string storedProcedureName, 
            object? parameters = null, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure and returns a scalar value
        /// </summary>
        Task<T?> ExecuteStoredProcedureScalarAsync<T>(
            string storedProcedureName, 
            object? parameters = null, 
            CancellationToken cancellationToken = default);
    }
}
