namespace ResultOf.Tests;

using FluentAssertions;

public class ResultOfSwitchShould
{
    [Fact]
    public void SwitchSuccessValue()
    {
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);
        
        var action = () => result.Switch<Todo>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchErrors()
    {
        ResultOf result = new List<Error>
        {
            Error.Conflict(), 
            Error.Conflict()
        };

        var action = () => result.Switch<Todo>(
            onSuccess: DoNotCall,
            onError: errors =>
            {
                errors.Should().AllBeAssignableTo<Error>();
            });

        action.Should().NotThrow();
    }

    [Fact]
    public void SwitchFirstSuccess()
    {
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);

        var action = () => result.SwitchFirst<Todo>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchFirstError()
    {
        ResultOf result = Error.NotFound();

        var action = () => result.SwitchFirst<Todo>(
            onSuccess: DoNotCall,
            onError: error =>
            {
                error.Should().BeEquivalentTo(Error.NotFound());
            }
        );

        action.Should().NotThrow();
    }
    
    
    
    
    
    
    
    [Fact]
    public void SwitchSuccessValueValidation()
    {
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);
        
        var action = () => result.SwitchValidation<Todo>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchValidationErrors()
    {
        ResultOf result = new List<ValidationError>
        {
            new("Ident", "Code", "Description", ValidationSeverity.Error),
            new("Ident", "Code", "Description", ValidationSeverity.Warning),
        };

        var action = () => result.SwitchValidation<Todo>(
            onSuccess: DoNotCall,
            onError: errors =>
            {
                errors.Should().AllBeAssignableTo<ValidationError>();
            });

        action.Should().NotThrow();
    }

    [Fact]
    public void SwitchFirstSuccessValidation()
    {
        var expected = new Todo("value");
        ResultOf result = ResultOf.Success(expected);

        var action = () => result.SwitchFirstValidation<Todo>(
            onSuccess: s =>
            {
                s.Should().Be(expected);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchFirstValidationError()
    {
        var expected = new ValidationError("Ident", "Code", "Description", ValidationSeverity.Error);
        ResultOf result = expected;

        var action = () => result.SwitchFirstValidation<Todo>(
            onSuccess: DoNotCall,
            onError: error =>
            {
                error.Should().BeEquivalentTo(expected);
            }
        );

        action.Should().NotThrow();
    }
    

    static void DoNotCall(IEnumerable<Error> errors)
    {
        throw new Exception("Do not call this");
    }
    
    static void DoNotCall(Error error)
    {
        throw new Exception("Do not call this");
    }
    
    static void DoNotCall(IEnumerable<ValidationError> errors)
    {
        throw new Exception("Do not call this");
    }
    
    static void DoNotCall(ValidationError error)
    {
        throw new Exception("Do not call this");
    }

    static void DoNotCall(Todo todo)
    {
        throw new Exception("Do not call this");
    }

    record Todo(string Value);
}