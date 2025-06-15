using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Review_Rating.Data;
using System.Security.Claims;

namespace Review_Rating.Models
{
    [Authorize(Policy = "ForUsersOnly")]
    public class CommentHub : Hub
        {
            private readonly ApplicationDbContext _context;

            public CommentHub(ApplicationDbContext context) {
                _context = context;
            }

            public async Task PostComment(string content, string statement)
            {
                
                Comment comment = new Comment();
                comment.ContentID = int.Parse(content);
                comment.Statement = statement;
                string email = Context.User.FindFirst(ClaimTypes.Email).Value;
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                
                comment.UserName = user.UserName;
                comment.User = user;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                

                await Clients.All.SendAsync("ReceiveComment", user.UserName, statement);
            }

        }
    
}
