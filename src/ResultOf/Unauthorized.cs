namespace ResultOf;

public record Unauthorized<TValue> : ResultOf<TValue>
{
    public Unauthorized()  : base(ResultType.Error)
    {
        errors.Add(Error.Unauthorized());
    }
    
    public Unauthorized(Error error) : base(ResultType.Error)
    {
        errors.Add(error);
    }
}