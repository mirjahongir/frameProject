
using System.Collections.Generic;

namespace Jh.Core.ViewModels.Result
{
    public class QueryResult<T>
    {
        public long Count { get; set; }
        public List<T> Data { get; set; }
    }
}
