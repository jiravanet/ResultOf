namespace ResultOf;

public record Forbidden : ErrorOf
{
    public Forbidden()  : base(ResultType.Error, Error.Forbidden())
    {
    }
    
    public Forbidden(Error error) : base(ResultType.Error, error)
    {
    }

    protected override Error[] NoErrors { get; } = {Error.Forbidden()};


}