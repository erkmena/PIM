using System.Collections.Generic;

namespace PIM.Model
{
    public class BrandModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public IEnumerable<ProductModel> Products{ get; set; }
    }
}
