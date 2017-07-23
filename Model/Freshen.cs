using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model
{
    [Table("Freshen")]
    public class Freshen
    {
        public virtual long FreshenId { get; set; }

        public virtual DateTime beginTime { get; set; }

        public virtual DateTime endTime { get; set; }

        public virtual int IntervalTime { get; set; }
    }
}
