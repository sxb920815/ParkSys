using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public long UserId { get; set; }

        [MaxLength(32)]
        public string UserName { get; set; }

        [Required,MaxLength(32)]
        public string LoginName { get; set; }

        [Required,MaxLength(32)]
        public string PassWord { get; set; }

        public long Permission { get; set; }

        [MaxLength(256)]
        public string Note { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
    }
}
