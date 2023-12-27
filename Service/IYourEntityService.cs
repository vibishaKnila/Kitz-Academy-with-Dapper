using Academy.Model;

namespace Academy.Service
{
	public interface IYourEntityService
	{
		Task<IEnumerable<dynamic>> GetEntitiesAsync();
		Task<dynamic> GetEntityByIdAsync(int id);
	}
}