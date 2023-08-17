namespace ResultOf;

public record Unauthorized : ErrorOf
{
   
    public Unauthorized()  : base(ResultType.Error, Error.Unauthorized())
    {
    }
    
    public Unauthorized(Error error) : base(ResultType.Error, error)
    {
    }

    protected override Error[] NoErrors { get; } = { Error.Unauthorized() };
}