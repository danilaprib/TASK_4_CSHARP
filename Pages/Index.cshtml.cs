using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TASK_4_CSHARP.Data;

namespace TASK_4_CSHARP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TaskFourContext _context;

        public IndexModel(TaskFourContext context)
        {
            _context = context;
        }

        public List<User> _users { get; set; }

        public void OnGet()
        {
            _users = _context.Users.OrderByDescending(u => u.LastLoginTime).ToList();

        }

        public async Task<IActionResult> OnPostBlock(int[] selectedIds)
        {
            if (selectedIds != null)
            {
                var users = _context.Users.Where(u => selectedIds.Contains(u.UserId)).ToList();
                foreach (var user in users)
                {
                    user.IsBlocked = true;
                    user.Status = "Blocked";
                }
                await _context.SaveChangesAsync();

                var currentUserId = User.FindFirst("UserId")?.Value;
                if (selectedIds.Any(id => id.ToString() == currentUserId))
                {
                    await HttpContext.SignOutAsync("CookieAuth");
                    return RedirectToPage("/Login");
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnblock(int[] selectedIds)
        {
            if (selectedIds != null)
            {
                var users = _context.Users.Where(u => selectedIds.Contains(u.UserId)).ToList();
                foreach (var user in users)
                {
                    user.IsBlocked = false;
                    if (user.IsVerified)
                    {
                        user.Status = "Active";
                    }
                    else
                    {
                        user.Status = "Unverified";
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int[] selectedIds)
        {
            if (selectedIds != null)
            {
                var users = _context.Users.Where(u => selectedIds.Contains(u.UserId)).ToList();
                _context.Users.RemoveRange(users);
                await _context.SaveChangesAsync();

                var currentUserId = User.FindFirst("UserId")?.Value;
                if (selectedIds.Any(id => id.ToString() == currentUserId))
                {
                    await HttpContext.SignOutAsync("CookieAuth");
                    return RedirectToPage("/Login");
                }
            }
            return RedirectToPage();
        }
    }
}
