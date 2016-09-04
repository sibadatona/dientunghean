using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PagedResult<TEntity>
    {
        public IEnumerable<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
