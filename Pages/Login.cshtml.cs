using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TASK_4_CSHARP.Data;

namespace TASK_4_CSHARP.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TaskFourContext _context;
        public LoginModel(TaskFourContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return Page();
            }
            if (!user.IsBlocked)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Status", user.Status)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                user.LastLoginTime = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"{user.Name ?? ""} logged in successfully";

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
