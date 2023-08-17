namespace ResultOf;

public record Conflict : ErrorOf
{
    
    public Conflict() : base(ResultType.Error, Error.Conflict())
    {
    }

    public Conflict(Error error) : base(ResultType.Error, error)
    {
    }

    protected override Error[] NoErrors { get; } = {Error.Conflict()};
}