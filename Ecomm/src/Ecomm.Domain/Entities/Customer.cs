using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Domain.Entities
{
    [Table("customers")]
    public class Customer
    {
        [Column("id")]
        public Guid Id { get; set; }        
        [Column("fullname")]
        public string FullName { get; set; } = null!;        
        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
