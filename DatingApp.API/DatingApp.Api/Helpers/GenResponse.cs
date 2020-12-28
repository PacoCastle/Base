using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Api.Helpers
{
    public class GenResponse<T>
    {
        public T DataResponse { get; set; }
        public List<MessagesResponseList> Messages { get; set; }
        public bool Success { get; set; }
    }
    public class MessagesResponseList
    {
        public string Message { get; set; }
        public int MesageID { get; set; }
    }
}
