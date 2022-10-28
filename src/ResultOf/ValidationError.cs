namespace ResultOf;


public readonly record struct ValidationError(string Identifier, string Code, string Description,
    ValidationSeverity Severity);