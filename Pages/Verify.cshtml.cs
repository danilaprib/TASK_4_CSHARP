using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TASK_4_CSHARP.Data;

namespace TASK_4_CSHARP.Pages
{
    public class VerifyModel : PageModel
    {
        private readonly TaskFourContext _context;

        public VerifyModel(TaskFourContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet(string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.VerificationToken == token);

            if (user != null && !user.IsBlocked)
            {
                user.VerificationToken = null;
                user.Status = "Active";
                user.IsVerified = true;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Account verified. You are now active.";
            }

            return RedirectToPage("Index");
        }
    }
}
