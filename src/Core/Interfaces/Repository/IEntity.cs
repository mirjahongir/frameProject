namespace Jh.Core.Interfaces.Repository
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
