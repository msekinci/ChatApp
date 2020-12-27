using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Database;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ChatDbContext _ctx;

        public HomeController(ChatDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            var chats = _ctx.Chats
            .Include(x => x.Users)
            .Where(x => !x.Users
                .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return View(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            });

            _ctx.Chats.Add(chat);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinChat(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                Role = UserRole.Member,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            _ctx.ChatUsers.Add(chatUser);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Chat", "Home", new {id = id});
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _ctx.Chats.Include(x => x.Messages).FirstOrDefault(x => x.Id == id);
            if (chat == null)
            {
                return BadRequest("Not found chat by this room");
            }
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int chatId, string text)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = text,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };
            _ctx.Messages.Add(Message);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Chat", new { id = chatId });
        }
    }
}