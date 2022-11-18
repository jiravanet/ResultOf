namespace ResultOf;

[GenerateSerializer]
public record SuccessOf<TValue> : ResultOf<TValue>
{
    public SuccessOf(TValue value) : base(value) 
    {
    }
}