using System.Collections.Generic;

namespace PIM.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }

    }
}
