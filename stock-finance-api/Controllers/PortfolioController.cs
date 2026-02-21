using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stock_finance_api.Extensions;
using stock_finance_api.Interfaces;
using stock_finance_api.Models;
using stock_finance_api.Repository;

namespace stock_finance_api.Controllers
{
	[Route("api/portfolio")]
	[ApiController]
	public class PortfolioController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IStockRepository _stockRepo;
		private readonly IPortfolioRepository _portfolioRepo;

		public PortfolioController(UserManager<AppUser> userManager,
		IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
		{
			_userManager = userManager;
			_stockRepo = stockRepo;
			_portfolioRepo = portfolioRepo;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetUserPortfolio()
		{
			var username = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(username);
			var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
			return Ok(userPortfolio);
		}

		[HttpPost]
		public async Task<IActionResult> AddPortfolio(string symbol)
		{
			var username = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(username);
			var stock = await _stockRepo.GetBySymbolAsync(symbol);

			if (stock == null) return BadRequest("Stock not found");

			var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

			if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

			var portfolioModel = new Portfolio
			{
				StockId = stock.Id,
				AppUserId = appUser.Id
			};

			await _portfolioRepo.CreateAsync(portfolioModel);

			if (portfolioModel == null)
			{
				return StatusCode(500, "Could not create");
			}
			else
			{
				return Created();
			}
		}
	}
}
