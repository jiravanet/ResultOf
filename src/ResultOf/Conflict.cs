namespace ResultOf;

public record Conflict<TValue> : ResultOf<TValue>
{
    public Conflict() : base(ResultType.Error)
    {
        errors.Add(Error.Conflict());
    }

    public Conflict(Error error) : base(ResultType.Error)
    {
        errors.Add(error);
    }
}