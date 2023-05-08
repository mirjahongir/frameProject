
using System.Collections.Generic;

namespace Jh.Core.ViewModels.Request
{
    public interface IQueryResult<T>
    {
        public bool IsSuccess { get; set; }
        public long Count { get; set; }
        public List<T> Data { get; set; }
    }

}
