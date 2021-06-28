using PIM.Model.DTOs;
using System.Collections.Generic;

namespace PIM.Model.Responses
{
    public class GetUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<ProductDTOModel> Products{ get; set; }

    }
}
