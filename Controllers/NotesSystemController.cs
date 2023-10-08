using Microsoft.AspNetCore.Mvc;
using code_wizards_website.Models;
using code_wizards_website.Services;
using System.Security.Claims;
using Newtonsoft.Json;

namespace code_wizards_website.Controllers
{
    public class NotesSystemController : Controller
    {
        public async Task<IActionResult> CreateNote(NoteModel note)
        {
            List<UserModel> users = await NotesAPIService.GetAllUsersAsync();
            note.UserId = users.FirstOrDefault(x => x.Email == User.FindFirst(ClaimTypes.Email).Value).Id;

            await NotesAPIService.AddNoteAsync(note);

            return RedirectToAction("Index", "Content");
        }
    }
}