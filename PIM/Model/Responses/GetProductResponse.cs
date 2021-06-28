using PIM.Model.DTOs;

namespace PIM.Model.Responses
{
    public class GetProductResponse
    {
        public string ProductName { get; set; }
        public string BrandName{ get; set; }
        public int BrandId { get; set; }
    }
}
