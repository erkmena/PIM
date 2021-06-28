using System.Collections.Generic;

namespace PIM.Model.DTOs
{
    public class UserDTOModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<ProductDTOModel> Products { get; set; }
    }
}
