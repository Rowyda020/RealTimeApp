using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTime.Models;
using System;

namespace RealTime.Hubs
{
    public class commentsHub:Hub
    {
        private readonly ProdDB _context;

        public commentsHub(ProdDB context)
        {
            _context = context;
        }
        public async Task ProductGroup(int productId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"product_{productId}");
        }

        public async Task SendComment(int productId, string userName, string content)
        {
            var comment = new Comment
            {
                ProductId = productId,
                UserName = userName,
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            await Clients.Group($"product_{productId}")
                .SendAsync("NewCommentNotify", productId, userName, content, comment.CreatedAt);
        }

    }
}
