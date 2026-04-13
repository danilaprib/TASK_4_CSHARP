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
            if (selectedIds != null && selectedIds.Length > 0)
            {
                var users = _context.Users.Where(u => selectedIds.Contains(u.UserId)).ToList();

                foreach (var user in users)
                {
                    user.IsBlocked = true;
                    user.Status = "Blocked";
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
