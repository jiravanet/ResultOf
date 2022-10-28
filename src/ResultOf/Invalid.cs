namespace ResultOf;

public record Invalid<TValue> : ResultOf<TValue>
{
	public Invalid(ValidationError validationError) : base(ResultType.Error)
	{
		validationErrors.Add(validationError);
	}

	public Invalid(IEnumerable<ValidationError> validationErrors) : base(ResultType.Error)
	{
		this.validationErrors.AddRange(validationErrors);
	}
}