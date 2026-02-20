using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace stock_finance_api.Models
{
	[Table("AppUser")]
	public class AppUser : IdentityUser
	{
		public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
	}
}
