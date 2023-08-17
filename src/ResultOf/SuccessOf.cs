namespace ResultOf;

public record SuccessOf<TResult> : ResultOf
{
    public SuccessOf(TResult value) : base(ResultType.Ok)
    {
        Value = value;
    }

    public TResult Value { get; }
}

public abstract record ErrorOf : ResultOf
{
    protected readonly List<Error> errors = new();

    protected ErrorOf(ResultType resultType) : base(resultType)
    {
    }

    protected ErrorOf(ResultType resultType, Error error) : base(resultType)
    {
        errors.Add(error);
    }
    
    protected abstract Error[] NoErrors { get; }
    public IEnumerable<Error> Errors => errors.Count > 0 
        ? errors.AsReadOnly() 
        : NoErrors;
}