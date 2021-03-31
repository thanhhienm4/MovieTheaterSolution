using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Common.Paging
{
    public class PageResult<T> : PageResultBase
    {
        public List<T> Item { get; set; }
        
    }
}
