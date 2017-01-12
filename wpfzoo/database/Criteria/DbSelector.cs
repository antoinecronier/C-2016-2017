using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.database.criteria
{
    public enum DbSelector
    {
        [StringValue("*")]
        ALL,
        [StringValue("")]
        NONE,
    }
}
