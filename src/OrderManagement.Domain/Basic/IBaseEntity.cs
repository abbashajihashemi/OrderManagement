namespace OrderManagement.Domain.Basic;

public interface IBaseEntity;

public interface IBaseEntity<TKey> : IBaseEntity where TKey : struct
{
    public TKey Id { get; set; }
}