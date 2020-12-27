using System.Linq;
using System.Security.Claims;
using ChatApp.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly ChatDbContext _ctx;

        public RoomViewComponent(ChatDbContext ctx)
        {
            _ctx = ctx;
        }
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _ctx.ChatUsers
            .Include(x => x.Chat)
            .Where(x => x.UserId == userId)
            .Select(x => x.Chat);
            return View(chats);
        }
    }
}