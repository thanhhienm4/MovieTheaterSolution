using System.Collections.Generic;

namespace MovieTheater.Models.Common.Paging
{
    public class PageResult<T> : PageResultBase
    {
        public PageResult()
        {
            Item = new List<T>();
        }
        public List<T> Item { get; set; }
    }
}