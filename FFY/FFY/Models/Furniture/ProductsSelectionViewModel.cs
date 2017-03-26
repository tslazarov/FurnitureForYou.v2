using System.Collections.Generic;

namespace FFY.Web.Models.Furniture
{
    public class ProductsSelectionViewModel
    {
        public string FilterBy { get; set; }

        public string Search { get; set; }

        public int? From { get; set; }

        public int? To { get; set; }

        public int ProductsCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public IEnumerable<SingleProductSelectionViewModel> Products { get; set; }
    }
}