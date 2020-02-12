using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admiral.Server.Domain.Entities.Identity {
    public class UserIdentity : EntityBase
    {
        public UserIdentity()
        {

        }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public bool IsPasswordExpired { get; set; } = false;

        public bool ForceChangePassword { get; set; }

        public bool CanUserResetExpiredPassword { get; set; } = true;

        public DateTime PasswordExpiresAt { get; set; } = DateTime.UtcNow.AddYears(1);
    }
}
