namespace ResultOf;

public interface IResultOf 
{
    bool IsSuccess { get; }

    ResultType ResultType { get; }
	
    IEnumerable<Error> Errors { get; }
	
    IEnumerable<ValidationError> ValidationErrors { get; }
	
}