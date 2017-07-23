using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model
{
    [Table("AreaAndEqu")]
    public class AreaAndEqu
    {
        [Key]
        public virtual long Id { get; set; }

        
        public virtual long AreaId { get; set; }

        public virtual long EquipmentId { get; set; }

        public virtual string State { get;set; }


    }
}
