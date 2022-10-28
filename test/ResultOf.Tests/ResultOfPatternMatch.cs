namespace ResultOf.Tests;

using FluentAssertions;

public class ResultOfPatternMatchShould
{

    [Fact]
    public void MatchSuccess()
    {
        ResultOf<Todo> result = ResultOf<Todo>.Success(new Todo("value"));

        var action = () =>
        {
            switch (result)
            {
                case SuccessOf<Todo> success:
                    success.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }
    
    [Fact]
    public void MatchConflict()
    {
        ResultOf<Todo> result = ResultOf<Todo>.Conflict();

        var action = () =>
        {
            switch (result)
            {
                case Conflict<Todo> c:
                    c.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }
    
    [Fact]
    public void MatchFault()
    {
        ResultOf<Todo> result = ResultOf<Todo>.Fault(Error.Fault());

        var action = () =>
        {
            switch (result)
            {
                case Fault<Todo> c:
                    c.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }
    
    [Fact]
    public void MatchForbidden()
    {
        ResultOf<Todo> result = ResultOf<Todo>.Forbidden();

        var action = () =>
        {
            switch (result)
            {
                case Forbidden<Todo> c:
                    c.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }
    
    [Fact]
    public void MatchInvalid()
    {
        ResultOf<Todo> result =
            ResultOf<Todo>.Invalid(new ValidationError("Ident", "Code", "Description", ValidationSeverity.Info));

        var action = () =>
        {
            switch (result)
            {
                case Invalid<Todo> c:
                    c.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }
    
    
    [Fact]
    public void MatchNotFound()
    {
        ResultOf<Todo> result = ResultOf<Todo>.NotFound();

        var action = () =>
        {
            switch (result)
            {
                case NotFound<Todo> c:
                    c.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }
    
    
    [Fact]
    public void MatchUnauthorized()
    {
        ResultOf<Todo> result = ResultOf<Todo>.Unauthorized();

        var action = () =>
        {
            switch (result)
            {
                case Unauthorized<Todo> c:
                    c.Should().Be(result);
                    return;
                default:
                    DoNotCall();
                    break;
            }
        };
        action.Should().NotThrow();
    }

    static void DoNotCall()
    {
        throw new Exception("Do not call this");
    }

    record Todo(string Value);
}