using System.Collections.Generic;

namespace PIM.Model.DTOs
{
    public class BrandDTOModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public IEnumerable<ProductDTOModel> Products{ get; set; }
    }
}
