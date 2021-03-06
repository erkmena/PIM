using System.Collections.Generic;
using System.Linq;

namespace PIM.Model.Responses
{
    public class BaseResponse<TData>
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }

        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public TData Data { get; set; }
    }
}
