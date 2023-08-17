namespace ResultOf.Tests;

public class ResultOfMatchShould
{
    [Fact]
    public void MatchSuccessValue()
    {
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);
        
        var action = () => result.Match<Todo, string>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchErrors()
    {
        ResultOf result = new List<Error>
        {
            Error.Conflict(), 
            Error.Conflict()
        };

        var action = () => result.Match<Todo, string>(
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
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);

        var action = () => result.MatchFirst<Todo, string>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchFirstError()
    {
        ResultOf result = Error.NotFound();

        var action = () => result.MatchFirst<Todo, string>(
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
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);
        
        var action = () => result.MatchValidation<Todo, string>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchValidationErrors()
    {
        ResultOf result = new List<ValidationError>
        {
            new("Ident", "Code", "Description", ValidationSeverity.Error),
            new("Ident", "Code", "Description", ValidationSeverity.Warning),
        };

        var action = () => result.MatchValidation<Todo, string>(
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
        var expected =   new Todo("value");
        ResultOf result = ResultOf.Success(expected);

        var action = () => result.MatchFirstValidation<Todo, string>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
                return "passed";
            },
            onError: DoNotCall);

        action.Should().NotThrow().Subject.Should().Be("passed");
    }
    
    [Fact]
    public void MatchFirstValidationError()
    {
        var expected = new ValidationError("Ident", "Code", "Description", ValidationSeverity.Error);
        ResultOf result = expected;

        var action = () => result.MatchFirstValidation<Todo, string>(
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