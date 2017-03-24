using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models
{
    public class SearchModel
    {
        public string SearchWord { get; set; }

        public string SortBy { get; set; }

        public string FilterBy { get; set; }
    }
}