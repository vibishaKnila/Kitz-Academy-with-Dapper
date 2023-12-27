using Dapper;
using System.Data;

namespace Academy.Repository
{
	public interface IGenericRepository
	{
		Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		Task<T> QuerySingleOrDefaultAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
	}
}