namespace ResultOf.Tests;

using FluentAssertions;

public class ResultOfPatternMatchShould
{

    [Fact]
    public void MatchSuccess()
    {
        ResultOf result = ResultOf.Success(new Todo("value"));

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
        ResultOf result = ResultOf.Conflict();

        var action = () =>
        {
            switch (result)
            {
                case Conflict c:
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
        ResultOf result = ResultOf.Fault(Error.Fault());

        var action = () =>
        {
            switch (result)
            {
                case Fault c:
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
        ResultOf result = ResultOf.Forbidden();

        var action = () =>
        {
            switch (result)
            {
                case Forbidden c:
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
        ResultOf result =
            ResultOf.Invalid(new ValidationError("Ident", "Code", "Description", ValidationSeverity.Info));

        var action = () =>
        {
            switch (result)
            {
                case Invalid c:
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
        ResultOf result = ResultOf.NotFound();

        var action = () =>
        {
            switch (result)
            {
                case NotFound c:
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
        ResultOf result = ResultOf.Unauthorized();

        var action = () =>
        {
            switch (result)
            {
                case Unauthorized c:
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