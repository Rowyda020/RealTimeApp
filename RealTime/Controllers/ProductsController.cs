using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTime.Hubs;
using RealTime.Models;

namespace RealTime.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProdDB _context;
        private readonly IHubContext<commentsHub> _hubContext;
        public ProductsController(ProdDB context, IHubContext<commentsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> AddComment(int productId, string username, string content)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = content,
                    CreatedAt = DateTime.Now,
                    ProductId = productId,
                    UserName = username
                };
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();


                await _hubContext.Clients.Group($"product_{productId}")
     .SendAsync("NewCommentNotify", productId, username, content, DateTime.Now);


                return RedirectToAction(nameof(Details), new { id = productId });
            }
            return RedirectToAction(nameof(Details), new { id = productId });
        }

    }
}
