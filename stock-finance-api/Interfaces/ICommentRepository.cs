using stock_finance_api.Models;

namespace stock_finance_api.Interfaces
{
	public interface ICommentRepository
	{
		Task<List<Comment>> GetAllAsync();
	}
}
