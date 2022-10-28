namespace ResultOf.Tests;

public class ResultOfMatchShould
{
    [Fact]
    public void MatchSuccessValue()
    {
        ResultOf<Todo> result = new Todo("value");
        
        var action = () => result.Match(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchErrors()
    {
        ResultOf<Todo> result = new List<Error>
        {
            Error.Conflict(), 
            Error.Conflict()
        };

        var action = () => result.Match(
            onSuccess: DoNotCall,
            onError: errors =>
            {
                errors.Should().AllBeAssignableTo<Error>();
                return "errors";
            });

        action.Should().NotThrow().Subject.Should().Be("errors");
    }

    [Fact]
    public void MatchFirstSuccess()
    {
        ResultOf<Todo> result =  new Todo("value");

        var action = () => result.MatchFirst(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchFirstError()
    {
        ResultOf<Todo> result = Error.NotFound();

        var action = () => result.MatchFirst(
            onSuccess: DoNotCall,
            onError: error =>
            {
                error.Should().BeEquivalentTo(Error.NotFound());
                return "error";
            }
        );

        action.Should().NotThrow().Subject.Should().Be("error");
    }
    
    
    
    
    
    
    
    [Fact]
    public void MatchSuccessValueValidation()
    {
        ResultOf<Todo> result = new Todo("value");
        
        var action = () => result.MatchValidation(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchValidationErrors()
    {
        ResultOf<Todo> result = new List<ValidationError>
        {
            new("Ident", "Code", "Description", ValidationSeverity.Error),
            new("Ident", "Code", "Description", ValidationSeverity.Warning),
        };

        var action = () => result.MatchValidation(
            onSuccess: DoNotCall,
            onError: errors =>
            {
                errors.Should().AllBeAssignableTo<ValidationError>();
                return "errors";
            });

        action.Should().NotThrow().Subject.Should().Be("errors");
    }

    [Fact]
    public void MatchFirstSuccessValidation()
    {
        ResultOf<Todo> result =  new Todo("value");

        var action = () => result.MatchFirstValidation(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchFirstValidationError()
    {
        var expected = new ValidationError("Ident", "Code", "Description", ValidationSeverity.Error);
        ResultOf<Todo> result = expected;

        var action = () => result.MatchFirstValidation(
            onSuccess: DoNotCall,
            onError: error =>
            {
                error.Should().BeEquivalentTo(expected);
                return "error";
            }
        );

        action.Should().NotThrow().Subject.Should().Be("error");
    }
    

    static string DoNotCall(IEnumerable<Error> errors)
    {
        throw new Exception("Do not call this");
    }
    
    static string DoNotCall(Error error)
    {
        throw new Exception("Do not call this");
    }
    
    static string DoNotCall(IEnumerable<ValidationError> errors)
    {
        throw new Exception("Do not call this");
    }
    
    static string DoNotCall(ValidationError error)
    {
        throw new Exception("Do not call this");
    }

    static string DoNotCall(Todo todo)
    {
        throw new Exception("Do not call this");
    }

    record Todo(string Value);
}