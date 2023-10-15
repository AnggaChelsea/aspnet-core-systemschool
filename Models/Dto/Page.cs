using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class PageInate
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PageInate(int totalItemCount, int totalPageCount, int pageSize, int currentPage){
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageCount = (int)Math.Ceiling(totalPageCount / (double)pageSize);
        }
    }
}