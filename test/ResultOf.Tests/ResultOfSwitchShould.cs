namespace ResultOf.Tests;

using FluentAssertions;

public class ResultOfSwitchShould
{
    [Fact]
    public void SwitchSuccessValue()
    {
        ResultOf<Todo> result = new Todo("value");
        
        var action = () => result.Switch(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchErrors()
    {
        ResultOf<Todo> result = new List<Error>
        {
            Error.Conflict(), 
            Error.Conflict()
        };

        var action = () => result.Switch(
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
        ResultOf<Todo> result =  new Todo("value");

        var action = () => result.SwitchFirst(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchFirstError()
    {
        ResultOf<Todo> result = Error.NotFound();

        var action = () => result.SwitchFirst(
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
        ResultOf<Todo> result = new Todo("value");
        
        var action = () => result.SwitchValidation(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchValidationErrors()
    {
        ResultOf<Todo> result = new List<ValidationError>
        {
            new("Ident", "Code", "Description", ValidationSeverity.Error),
            new("Ident", "Code", "Description", ValidationSeverity.Warning),
        };

        var action = () => result.SwitchValidation(
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
        ResultOf<Todo> result =  new Todo("value");

        var action = () => result.SwitchFirstValidation(
            onSuccess: s =>
            {
                s.Should().Be(result.Value);
            },
            onError: DoNotCall);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void SwitchFirstValidationError()
    {
        var expected = new ValidationError("Ident", "Code", "Description", ValidationSeverity.Error);
        ResultOf<Todo> result = expected;

        var action = () => result.SwitchFirstValidation(
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