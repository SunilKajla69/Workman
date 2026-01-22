using Dapper;
using System.Data;

namespace WORKMAN.Config.Infrastructure.Dapper.Repositories
{
    /// <summary>
    /// Base implementation for executing stored procedures using Dapper
    /// </summary>
    public class DapperRepository : IDapperRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<DapperRepository> _logger;

        public DapperRepository(IDapperContext context, ILogger<DapperRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Executes a stored procedure and returns a single result
        /// </summary>
        public async Task<T?> ExecuteStoredProcedureSingleAsync<T>(
            string storedProcedureName,
            object? parameters = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = _context.CreateConnection();
                _logger.LogInformation("Executing stored procedure: {StoredProcedure}", storedProcedureName);
                
                var result = await connection.QueryFirstOrDefaultAsync<T>(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedureName);
                throw;
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns multiple results
        /// </summary>
        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(
            string storedProcedureName,
            object? parameters = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = _context.CreateConnection();
                _logger.LogInformation("Executing stored procedure: {StoredProcedure}", storedProcedureName);
                
                var result = await connection.QueryAsync<T>(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedureName);
                throw;
            }
        }

        /// <summary>
        /// Executes a stored procedure without returning results (INSERT, UPDATE, DELETE)
        /// </summary>
        public async Task<int> ExecuteStoredProcedureNonQueryAsync(
            string storedProcedureName,
            object? parameters = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = _context.CreateConnection();
                _logger.LogInformation("Executing stored procedure: {StoredProcedure}", storedProcedureName);
                
                var affectedRows = await connection.ExecuteAsync(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                _logger.LogInformation("Stored procedure {StoredProcedure} affected {Rows} rows", 
                    storedProcedureName, affectedRows);

                return affectedRows;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedureName);
                throw;
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns a scalar value
        /// </summary>
        public async Task<T?> ExecuteStoredProcedureScalarAsync<T>(
            string storedProcedureName,
            object? parameters = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = _context.CreateConnection();
                _logger.LogInformation("Executing stored procedure: {StoredProcedure}", storedProcedureName);
                
                var result = await connection.ExecuteScalarAsync<T>(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedureName);
                throw;
            }
        }
    }
}
