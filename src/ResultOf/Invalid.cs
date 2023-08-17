namespace ResultOf;

public record Invalid : ResultOf
{
	readonly List<ValidationError> validationErrors = new();

	public Invalid(ValidationError validationError) : base(ResultType.Error)
	{
		validationErrors.Add(validationError);
	}

	public Invalid(IEnumerable<ValidationError> validationErrors) : base(ResultType.Error)
	{
		this.validationErrors.AddRange(validationErrors);
	}
	
	public static readonly ValidationError NoValidationError = new("ResultOf.NoValidationErrors", "NoValidationErrors",
		"No validation errors",
		ValidationSeverity.Info);
    
	static readonly ValidationError[] NoValidationErrors =
	{
		NoValidationError
	};


	public IEnumerable<ValidationError> ValidationErrors => validationErrors.Count > 0 
		? validationErrors.AsReadOnly() 
		: NoValidationErrors;

}