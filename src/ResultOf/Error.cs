namespace ResultOf;

[GenerateSerializer]
public readonly record struct Error(string Code, string Description, int ErrorCode)
{
	public static Error NotFound(string code = "General.NotFound", string description = "Not found") =>
		new(code, description, (int)ErrorType.NotFound);
	
	public static Error Conflict(string code = "General.Conflict", string description = "Conflict") =>
		new(code, description, (int)ErrorType.Conflict);
	
	public static Error Fault(string code = "General.Fault", string description = "Fault") =>
		new(code, description, (int)ErrorType.Fault);
	
	public static Error Forbidden(string code = "General.Forbidden", string description = "Forbidden") =>
		new(code, description, (int)ErrorType.Forbidden);
	
	public static Error Unauthorized(string code = "General.Unauthorized", string description = "Unauthorized") =>
		new(code, description, (int)ErrorType.Unauthorized);
	
	public static Error Invalid(string code = "General.Invalid", string description = "Invalid") =>
		new(code, description, (int)ErrorType.Invalid);
	
	public static Error Custom(string code, string description, int errorCode = 0) =>
		new(code, description, errorCode);
}