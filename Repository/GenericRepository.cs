using Academy.Data;
using Academy.Model;
using Dapper;
using static Dapper.SqlMapper;
using System.Data;

namespace Academy.Repository
{
	public class GenericRepository : IGenericRepository
	{
		private readonly IDbConnection _connection;

		public GenericRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		public async Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{

			return await _connection.QueryAsync<T>(sql, parms, commandType: commandType);
		}

		public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{
			return await _connection.QueryFirstOrDefaultAsync<T>(sql, parms, commandType: commandType);
		}


		public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{
			return await _connection.QuerySingleOrDefaultAsync<T>(sql, parms, commandType: commandType);
		}


		public async Task<GridReader> QueryMultipleAsync(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
		{

			return await _connection.QueryMultipleAsync(sql, parms, commandType: commandType, commandTimeout: 0);
		}
	}


}

