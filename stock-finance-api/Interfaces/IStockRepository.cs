using stock_finance_api.Dtos.Stock;
using stock_finance_api.Models;

namespace stock_finance_api.Interfaces
{
	public interface IStockRepository
	{
		Task<List<Stock>> GetAllAsync();

		Task<Stock?> GetByIdAsync(int id);

		Task<Stock> CreateAsync(Stock stockModel);

		Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

		Task<Stock?> DeleteAsync(int id);

		Task<bool> StockExists(int id);
	}
}
