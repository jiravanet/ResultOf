namespace ResultOf;

public abstract record ResultOf<TValue> : IResultOf
{
    protected readonly TValue? value = default;
    protected readonly List<Error> errors = new();
    protected readonly List<ValidationError> validationErrors = new();

    protected ResultOf(TValue value) 
    {
        this.value = value;
        ResultType = ResultType.Ok;
    }

    protected ResultOf(ResultType resultType)
    {
        ResultType = resultType;
    }
    
    public TValue Value => value!;

    public bool IsSuccess => ResultType == ResultType.Ok;
	
    public ResultType ResultType { get; }

    public static readonly Error NoError = Error.Custom("ResultOf.NoErrors", "No error");
    
    static readonly Error[] NoErrors = { NoError };

    public static readonly ValidationError NoValidationError = new("ResultOf.NoValidationErrors", "NoValidationErrors",
	    "No validation errors",
	    ValidationSeverity.Info);
    
    static readonly ValidationError[] NoValidationErrors =
    {
	    NoValidationError
    };

    public IEnumerable<Error> Errors => errors.Count > 0 
        ? errors.AsReadOnly() 
        : NoErrors;

    public IEnumerable<ValidationError> ValidationErrors => validationErrors.Count > 0 
	    ? validationErrors.AsReadOnly() 
        : NoValidationErrors;

    public static implicit operator ResultOf<TValue>(TValue value) => new SuccessOf<TValue>(value);
	
    public static implicit operator ResultOf<TValue>(Error error) => new Fault<TValue>(error);
	
    public static implicit operator ResultOf<TValue>(List<Error> errors) => new Fault<TValue>(errors);
	
    public static implicit operator ResultOf<TValue>(ValidationError error) => new Invalid<TValue>(error);
	
    public static implicit operator ResultOf<TValue>(List<ValidationError> errors) => new Invalid<TValue>(errors);

    public static ResultOf<TValue> Success(TValue value) => new SuccessOf<TValue>(value);
	
    public static ResultOf<TValue> NotFound() => new NotFound<TValue>();
    public static ResultOf<TValue> NotFound(Error error) => new NotFound<TValue>(error);
	
    public static ResultOf<TValue> Conflict() => new Conflict<TValue>();
    public static ResultOf<TValue> Conflict(Error error) => new Conflict<TValue>(error);
	
    public static ResultOf<TValue> Forbidden() => new Forbidden<TValue>();
    public static ResultOf<TValue> Forbidden(Error error) => new Forbidden<TValue>(error);
	
    public static ResultOf<TValue> Unauthorized() => new Unauthorized<TValue>();
    public static ResultOf<TValue> Unauthorized(Error error) => new Unauthorized<TValue>(error);
	
    public static ResultOf<TValue> Fault(Error error) => new Fault<TValue>(error);
	
    public static ResultOf<TValue> Fault(IEnumerable<Error> errors) => new Fault<TValue>(errors);
	
    public static ResultOf<TValue> Invalid(ValidationError error) => new Invalid<TValue>(error);
	
    public static ResultOf<TValue> Invalid(IEnumerable<ValidationError> errors) => new Invalid<TValue>(errors);


    public void SwitchFirst(Action<TValue> onSuccess, Action<Error> onError)
    {
	    if (IsSuccess)
	    {
		    onSuccess(Value);
		    return;
	    }
	    onError(Errors.First());
    }
    
    public void Switch(Action<TValue> onSuccess, Action<IEnumerable<Error>> onError)
    {
	    if (IsSuccess)
	    {
		    onSuccess(Value);
		    return;
	    }
	    onError(Errors);
    }
    
    public void SwitchFirstValidation(Action<TValue> onSuccess, Action<ValidationError> onError)
    {
	    if (IsSuccess)
	    {
		    onSuccess(Value);
		    return;
	    }
	    onError(ValidationErrors.First());
    }
    
    public void SwitchValidation(Action<TValue> onSuccess, Action<IEnumerable<ValidationError>> onError)
    {
	    if (IsSuccess)
	    {
		    onSuccess(Value);
		    return;
	    }
	    onError(ValidationErrors);
    }
    
    public TResult MatchFirst<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onError)
    {
	    return IsSuccess 
		    ? onSuccess(Value) 
		    : onError(Errors.First());
    }
    
    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<IEnumerable<Error>, TResult> onError)
    {
	    return IsSuccess 
		    ? onSuccess(Value) 
		    : onError(Errors);
    }
    
    public TResult MatchFirstValidation<TResult>(Func<TValue, TResult> onSuccess, Func<ValidationError, TResult> onError)
    {
	    return IsSuccess 
		    ? onSuccess(Value) 
		    : onError(ValidationErrors.First());
    }
    
    public TResult MatchValidation<TResult>(Func<TValue, TResult> onSuccess, Func<IEnumerable<ValidationError>, TResult> onError)
    {
	    return IsSuccess 
		    ? onSuccess(Value) 
		    : onError(ValidationErrors);
    }
}