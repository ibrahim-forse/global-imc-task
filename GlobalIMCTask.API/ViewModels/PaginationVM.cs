using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalIMCTask.API.ViewModels
{
    public class PaginationVM
    {
        public int CurrentPage { set; get; }
        public double TotalPages { set; get; }
        public bool IsLastPage { set; get; }
    }
}
