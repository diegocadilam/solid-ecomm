using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("fullname")]
        public string FullName { get; set; } = null!;
        [Column("email")]
        public string Email { get; set; } = null!;
        [Column("passwordhash")]
        public string PasswordHash { get; set; } = null!;
        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
