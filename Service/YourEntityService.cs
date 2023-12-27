using Academy.Model;
using Academy.Repository;
using Dapper;
using System.Data;

namespace Academy.Service
{
	public class YourEntityService : IYourEntityService
	{
		private readonly IGenericRepository _genericRepository;

		public YourEntityService(IGenericRepository genericRepository)
		{
			_genericRepository = genericRepository;
		}

		public async Task<IEnumerable<dynamic>> GetEntitiesAsync()
		{
			var sp = "SELECT * FROM Users"; 
			var parameters = new DynamicParameters();

			return await _genericRepository.QueryAsync<dynamic>(sp, parameters, commandType: CommandType.Text);
		}

		public async Task<dynamic> GetEntityByIdAsync(int Id)
		{
			var sp = "SELECT * FROM dbo.Users WITH(NOLOCK) Where id=@Id";
			var parameters = new DynamicParameters();
			parameters.Add("Id", Id, DbType.Int32);

			return await _genericRepository.QuerySingleOrDefaultAsync<dynamic>(sp, parameters,commandType: CommandType.Text);
		}

	}
}
