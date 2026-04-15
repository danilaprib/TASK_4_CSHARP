using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net.Mail;
using System.Security.Claims;
using System.Net;
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
            var token = Guid.NewGuid().ToString();
            var user = new User
            {
                Name = username,
                Email = email,
                Password = password,
                Status = "Unverified",
                VerificationToken = token
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

            var verificationLink = $"https://task-4-csharp.onrender.com/Verify?token={token}";
            await SendVerificationEmailAsync(email, verificationLink);

            return RedirectToPage("Index");

        }
        private async Task SendVerificationEmailAsync(string recipientEmail, string link)
        {
            using var mail = new MailMessage();
            mail.From = new MailAddress("todolistapp7@gmail.com", "TASK_4_CSHARP");
            mail.To.Add(recipientEmail);
            mail.Subject = "TASK4 acc verification";
            mail.IsBodyHtml = true;
            mail.Body = $"Verify your account by this link: <a href='{link}'>Verify Account</a>";

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("todolistapp7@gmail.com", "wkjh vavj lukv dxiv "),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
