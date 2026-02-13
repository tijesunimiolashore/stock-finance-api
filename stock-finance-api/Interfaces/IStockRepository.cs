using stock_finance_api.Models;

namespace stock_finance_api.Interfaces
{
	public interface IStockRepository
	{
		Task<List<Stock>> GetAllAsync();
	}
}
