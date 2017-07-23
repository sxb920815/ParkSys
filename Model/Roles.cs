using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public  class Roles
    {
        public virtual long RolesId { get; set; }

        public virtual long Permisser { get; set; }
    }
}
