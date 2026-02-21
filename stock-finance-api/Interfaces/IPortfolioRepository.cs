using stock_finance_api.Models;

namespace stock_finance_api.Interfaces
{
	public interface IPortfolioRepository
	{
		Task<List<Stock>> GetUserPortfolio(AppUser user);

		Task<Portfolio> CreateAsync(Portfolio portfolio);
	}
}
