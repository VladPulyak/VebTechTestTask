using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Pagination
{
    public class PagedResult<TEntity>
        where TEntity : class
    {
        public TEntity? Data { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPage { get; set; }

        public int? TotalPages { get; set; }
    }
}
