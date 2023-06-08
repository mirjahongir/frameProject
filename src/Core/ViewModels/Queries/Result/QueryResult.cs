
using System.Collections.Generic;

namespace Jh.Core.ViewModels.Queries.Result
{
    public interface IQueryResult<T> : IBaseResult
    {
        long Count { get; set; }
        List<T> Data { get; set; }
    }
    public interface IQueryResult : IBaseResult
    {

    }

}
