using System.Linq;
using System.Threading.Tasks;
using ChatApp.Database;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers{
    public class HomeController : Controller{
        private readonly ChatDbContext _ctx;

        public HomeController(ChatDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name){
            _ctx.Chats.Add(new Chat
            {
               Name =  name,
               Type = ChatType.Room
            });
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("{room}")]
        public IActionResult Chat(string room){
            var chat = _ctx.Chats.Include(x => x.Messages).FirstOrDefault(x => x.Name.Equals(room));
            if (chat == null)
            {
                return BadRequest("Not found chat by this room");
            }
            return View(chat);
        }
    }
}