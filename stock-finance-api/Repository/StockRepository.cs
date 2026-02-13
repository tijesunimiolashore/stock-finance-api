using Microsoft.EntityFrameworkCore;
using stock_finance_api.Data;
using stock_finance_api.Interfaces;
using stock_finance_api.Models;

namespace stock_finance_api.Repository
{
	public class StockRepository : IStockRepository
	{
		private readonly ApplicationDbContext _context;

		public StockRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public Task<List<Stock>> GetAllAsync()
		{
			return _context.Stocks.ToListAsync();
		}
	}
}
