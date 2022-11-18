namespace ResultOf;

[GenerateSerializer]
public record Forbidden<TValue> : ResultOf<TValue>
{
    public Forbidden()  : base(ResultType.Error)
    {
        errors.Add(Error.Forbidden());
    }
    
    public Forbidden(Error error) : base(ResultType.Error)
    {
        errors.Add(error);
    }
}