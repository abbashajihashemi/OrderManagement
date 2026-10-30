namespace OrderManagement.Domain.Basic;

public class BaseEntity<TKey> : IBaseEntity<TKey> where TKey : struct
{
    public TKey Id { get; set; }
}