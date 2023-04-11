using System;

namespace Jh.Core.ViewModels.Request
{
    public abstract class Query<TKey>
        where TKey : struct
    {
        public TKey? Id { get; set; }
        public string? Name { get; set; }
        public int? PageSize { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? PageCount { get; set; }
        public abstract void Parse();
    }
}
