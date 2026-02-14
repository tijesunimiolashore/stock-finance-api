using Microsoft.EntityFrameworkCore;
using stock_finance_api.Data;
using stock_finance_api.Interfaces;
using stock_finance_api.Models;

namespace stock_finance_api.Repository
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ApplicationDbContext _context;

		public CommentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Comment>> GetAllAsync()
		{
			return await _context.Comments.ToListAsync();
		}

		public async Task<Comment?> GetByIdAsync(int id)
		{
			return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
