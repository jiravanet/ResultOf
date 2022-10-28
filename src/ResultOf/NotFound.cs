namespace ResultOf;

public record NotFound<TValue> : ResultOf<TValue>
{
    public NotFound() : base(ResultType.Error)
    {
        errors.Add(Error.NotFound());
    }
    
    public NotFound(Error error) : base(ResultType.Error)
    {
        errors.Add(error);
    }
}