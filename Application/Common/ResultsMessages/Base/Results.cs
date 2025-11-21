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
    private T _data = default!;

    protected Results(T data)
    {
        Data = data;
    }
    public T Data
    {
        get => (_data != null ? _data : default)!;
        set => _data = value;
    }
}