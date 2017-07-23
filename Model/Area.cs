using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model
{
    [Table("Area")]
    public class Area
    {
        [Key]
        public long AreaId { get; set; }

        [DataType("varchar"),MaxLength(32),Required]
        public string AreaName { get; set; }

       
        public int? ParkingNumber { get; set; }

        public int? RestParkingNumber { get; set; }

        [MaxLength(256)]
        public string Note { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
    }
}
