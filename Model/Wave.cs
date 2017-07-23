﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Wave")]
    public class Wave
    {
        [Key]
        public long WaveId { get; set; }

        [MaxLength(32)]
        public string WaveName { get; set; }

        public long? AreaId { get; set; }

        [MaxLength(32)]
        public string IP { get; set; }

        [MaxLength(256)]
        public string Note { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
    }
}
