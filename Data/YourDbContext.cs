using Microsoft.Data.SqlClient;
using System.Data;

namespace Academy.Data
{
	public class YourDbContext
	{
		private readonly IConfiguration _configuration;
		private readonly string connectionstring;
		private string v;

		public YourDbContext(IConfiguration configuration)
		{
			_configuration = configuration;
			connectionstring = this._configuration.GetConnectionString("DefaultConnection");

		}

		

		public IDbConnection CreateConnection() => new SqlConnection(connectionstring);
	}
}
