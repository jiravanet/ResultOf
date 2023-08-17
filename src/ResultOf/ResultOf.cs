namespace ResultOf;

public abstract record ResultOf : IResultOf
{
    protected ResultOf() 
    {
        ResultType = ResultType.Ok;
    }

    protected ResultOf(ResultType resultType)
    {
	    ResultType = resultType;
    }

    public bool IsSuccess
    {
	    get => ResultType == ResultType.Ok;
	    set => _ = value;
    }

    public ResultType ResultType { get; }

    
	
    public static implicit operator ResultOf(Error error) => new Fault(error);
	
    public static implicit operator ResultOf(List<Error> errors) => new Fault(errors);
	
    public static implicit operator ResultOf(ValidationError error) => new Invalid(error);
	
    public static implicit operator ResultOf(List<ValidationError> errors) => new Invalid(errors);

    public static ResultOf Success<TResult>(TResult value) => new SuccessOf<TResult>(value);
	
    public static ResultOf NotFound() => new NotFound();
    public static ResultOf NotFound(Error error) => new NotFound(error);
	
    public static ResultOf Conflict() => new Conflict();
    public static ResultOf Conflict(Error error) => new Conflict(error);
	
    public static ResultOf Forbidden() => new Forbidden();
    public static ResultOf Forbidden(Error error) => new Forbidden(error);
	
    public static ResultOf Unauthorized() => new Unauthorized();
    public static ResultOf Unauthorized(Error error) => new Unauthorized(error);
	
    public static ResultOf Fault(Error error) => new Fault(error);
	
    public static ResultOf Fault(IEnumerable<Error> errors) => new Fault(errors);
	
    public static ResultOf Invalid(ValidationError error) => new Invalid(error);
	
    public static ResultOf Invalid(IEnumerable<ValidationError> errors) => new Invalid(errors);
    
}

public static class ResultOfExtensions
{
	    public static void SwitchFirst<TResult>(this ResultOf resultOf, Action<TResult> onSuccess, Action<Error> onError)
        {
	        switch (resultOf)
	        {
			     case SuccessOf<TResult> successOf:
			        onSuccess(successOf.Value!);
			        break;
                case ErrorOf errorOf:
					onError(errorOf.Errors.First());
			        break;
	        }
        }
        
        public static void Switch<TResult>(this ResultOf resultOf, Action<TResult> onSuccess, Action<IEnumerable<Error>> onError)
        {
	        switch (resultOf)
	        {
		        case SuccessOf<TResult> successOf:
			        onSuccess(successOf.Value!);
			        break;
		        case ErrorOf errorOf:
			        onError(errorOf.Errors);
			        break;
	        }
        }
        
        public static void SwitchFirstValidation<TResult>(this ResultOf resultOf, Action<TResult> onSuccess, Action<ValidationError> onError)
        {
	        switch (resultOf)
	        {
		        case SuccessOf<TResult> successOf:
			        onSuccess(successOf.Value!);
			        break;
		        case Invalid invalid:
			        onError(invalid.ValidationErrors.First());
			        break;
	        }
        }
        
        public static void SwitchValidation<TResult>(this ResultOf resultOf, Action<TResult> onSuccess, Action<IEnumerable<ValidationError>> onError)
        {
	        switch (resultOf)
	        {
		        case SuccessOf<TResult> successOf:
			        onSuccess(successOf.Value!);
			        break;
		        case Invalid invalid:
			        onError(invalid.ValidationErrors);
			        break;
	        }
        }
        
        public static TOutput? MatchFirst<TResult, TOutput>(this ResultOf resultOf, Func<TResult, TOutput> onSuccess, Func<Error, TOutput> onError)
        {
	        return resultOf switch
	        {
		        SuccessOf<TResult> successOf => onSuccess(successOf.Value!),
		        ErrorOf errorOf => onError(errorOf.Errors.First()),
		        _ => default
	        };
        }
        
        public static TOutput? Match<TResult, TOutput>(this ResultOf resultOf, Func<TResult, TOutput> onSuccess, Func<IEnumerable<Error>, TOutput> onError)
        {
	        return resultOf switch
	        {
		        SuccessOf<TResult> successOf => onSuccess(successOf.Value!),
		        ErrorOf errorOf => onError(errorOf.Errors),
		        _ => default
	        };
        }
        
        public static TOutput? MatchFirstValidation<TResult, TOutput>(this ResultOf resultOf, Func<TResult, TOutput> onSuccess, Func<ValidationError, TOutput> onError)
        {
	        return resultOf switch
	        {
		        SuccessOf<TResult> successOf => onSuccess(successOf.Value!),
		        Invalid invalid => onError(invalid.ValidationErrors.First()),
		        _ => default
	        };
        }
        
        public static TOutput? MatchValidation<TResult, TOutput>(this ResultOf resultOf, Func<TResult, TOutput> onSuccess, Func<IEnumerable<ValidationError>, TOutput> onError)
        {
	        return resultOf switch
	        {
		        SuccessOf<TResult> successOf => onSuccess(successOf.Value!),
		        Invalid invalid => onError(invalid.ValidationErrors),
		        _ => default
	        };
        }
} 