using System;

namespace Jh.Core.ViewModels.Queries.Request
{
    public interface IQuery
    {
        string Id { get; set; }
        string? Name { get; set; }
        int PageSize { get; set; }
        DateTime? From { get; set; }
        DateTime? To { get; set; }
        int PageNumber { get; set; }
    }
}
