using System.Linq;
using ChatApp.Database;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.ViewComponents{
    public class RoomViewComponent : ViewComponent{
        private readonly ChatDbContext _ctx;

        public RoomViewComponent(ChatDbContext ctx)
        {
            _ctx = ctx;
        }
        public IViewComponentResult Invoke(){
            var chats = _ctx.Chats.ToList();
            return View(chats);
        }
    }
}