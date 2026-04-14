using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using TASK_4_CSHARP.Data;

namespace TASK_4_CSHARP.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly TaskFourContext _context;

        public SignUpModel(TaskFourContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPost(string username, string email, string password)
        {
            var user = new User
            {
                Name = username,
                Email = email,
                Password = password,
                Status = "Unverified"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
            await HttpContext.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("Index");
        }
    }
}
