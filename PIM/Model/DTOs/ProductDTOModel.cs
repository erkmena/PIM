namespace PIM.Model.DTOs
{
    public class ProductDTOModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
