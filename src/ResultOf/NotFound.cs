namespace ResultOf;

public record NotFound : ErrorOf
{
    public NotFound() : base(ResultType.Error, Error.NotFound())
    {
    }
    
    public NotFound(Error error) : base(ResultType.Error, error)
    {
    }

    protected override Error[] NoErrors { get; } = {Error.NotFound()};
}