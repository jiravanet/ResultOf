namespace ResultOf;

[GenerateSerializer]
public record Fault<TValue> : ResultOf<TValue>
{
    public Fault(Error error) : base(ResultType.Error)
    {
        errors.Add(error);
    }

    public Fault(IEnumerable<Error> errors) : base(ResultType.Error)
    {
        this.errors.AddRange(errors);
    }
}