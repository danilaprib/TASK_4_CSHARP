using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASK_4_CSHARP.Data
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Column("status")]
        public string Status { get; set; } = "Unverified";

        [Column("is_blocked")]
        public bool IsBlocked { get; set; } = false;

        [Column("last_login_time")]
        public DateTime? LastLoginTime { get; set; }

        [Column("registration_time")]
        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
    }
}