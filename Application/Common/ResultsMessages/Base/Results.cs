namespace Application.Common;

public abstract class Results
{
    public bool Success { get; protected set; }
    public string? Message { get; set; }
    public int Code { get; set; }
    public int ResultCode { get; set; }
    public ServiceError? Error { get; set; }

}

public abstract class Results<T> : Results
{
    private T _entity = default!;

    protected Results(T entity)
    {
        Entity = entity;
    }
    public T Entity
    {
        get => (_entity != null ? _entity : default)!;
        set => _entity = value;
    }
}