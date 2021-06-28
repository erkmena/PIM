using PIM.Model.DTOs;
using System.Collections.Generic;

namespace PIM.Model.Responses
{
    public class GetBrandResponse
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<ProductDTOModel> Products{ get; set; }
    }
}
