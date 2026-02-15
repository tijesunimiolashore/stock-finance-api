using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stock_finance_api.Data;
using stock_finance_api.Dtos.Comment;
using stock_finance_api.Interfaces;
using stock_finance_api.Mappers;

namespace stock_finance_api.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ICommentRepository _commentRepo;
		private readonly IStockRepository _stockRepo;

		public CommentController(ApplicationDbContext context, ICommentRepository commentRepo, IStockRepository stockRepo)
		{
			_commentRepo = commentRepo;
			_stockRepo = stockRepo;
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			if (!ModelState.IsValid) 
				return BadRequest(ModelState);

			var comments = await _commentRepo.GetAllAsync();

			var commentDto = comments.Select(c => c.ToCommentDto());

			return Ok(commentDto);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var comment = await _commentRepo.GetByIdAsync(id);

			if (comment == null)
			{
				return NotFound();
			}

			return Ok(comment.ToCommentDto());
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _stockRepo.StockExists(stockId))
			{
				return BadRequest($"Stock does not exist.");
			}

			var commentModel = commentDto.ToCommentfromCreate(stockId);
			await _commentRepo.CreateAsync(commentModel);
			return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

		}

		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentfromUpdate());

			if (comment == null)
			{
				return NotFound();
			}

			return Ok(comment.ToCommentDto());
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var commentModel = await _commentRepo.DeleteAsync(id);

			if (commentModel == null)
			{
				return NotFound("Comment does not exist");
			}

			return Ok(commentModel.ToCommentDto());
		}
	}
}
