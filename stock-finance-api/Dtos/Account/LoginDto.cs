using System.ComponentModel.DataAnnotations;

namespace stock_finance_api.Dtos.Account
{
	public class LoginDto
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
