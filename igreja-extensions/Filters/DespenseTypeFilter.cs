using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.Filters
{
    public class DespenseTypeFilter
    {
        public string UserId { get; set; }

        public DespenseTypeFilter()
        {

            UserId = string.Empty;
        }
    }
}
