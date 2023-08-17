namespace ResultOf;

public record Fault : ErrorOf
{

    public Fault(Error error) : base(ResultType.Error, error)
    {
    }

    public Fault(IEnumerable<Error> errors) : base(ResultType.Error)
    {
        base.errors.AddRange(errors);
    }

    protected override Error[] NoErrors { get; } = { Error.Custom("ResultOf.NoErrors", "No error")};
}