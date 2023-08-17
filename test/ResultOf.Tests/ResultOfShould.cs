namespace ResultOf.Tests;

using FluentAssertions;

public class ResultOfShould
{

    [Fact]
    public void CreateSuccess()
    {
        ResultOf result = ResultOf.Success("value");
        
        result.IsSuccess.Should().BeTrue();
        result.Should().BeOfType<SuccessOf<string>>();
        result.As<SuccessOf<string>>().Value.Should().Be("value");
    }
    

    [Fact]
    public void CreateNotFound()
    {
        var result = ResultOf.NotFound();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<NotFound>();
    }
    
    [Fact]
    public void CreateNotFoundOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf.NotFound(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<NotFound>();
        result.As<NotFound>().Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateConflict()
    {
        var result = ResultOf.Conflict();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Conflict>();
    }
    
    [Fact]
    public void CreateConflictOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf.Conflict(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Conflict>();
        result.As<Conflict>().Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateForbidden()
    {
        var result = ResultOf.Forbidden();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Forbidden>();
    }
    
    [Fact]
    public void CreateForbiddenOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf.Forbidden(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Forbidden>();
        result.As<Forbidden>().Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateUnauthorized()
    {
        var result = ResultOf.Unauthorized();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Unauthorized>();
    }
    
    [Fact]
    public void CreateUnauthorizedOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf.Unauthorized(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Unauthorized>();
        result.As<Unauthorized>().Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateFault()
    {
        var result = ResultOf.Fault(Error.Fault());

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Fault>();
        result.As<Fault>().Errors.Should().ContainSingle().Which.Should().Be(Error.Fault());
    }
    
    [Fact]
    public void CreateFaultWithListOfErrors()
    {
        var expected = new List<Error>
        {
            Error.Fault(),
            Error.Fault()
        };
        var result = ResultOf.Fault(expected);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Fault>();
        result.As<Fault>().Errors.Should().AllBeAssignableTo<Error>();
        result.As<Fault>().Errors.Should().HaveCount(expected.Count);
    }
    
    [Fact]
    public void CreateListOfErrorsFromImplicitCast()
    {
        var errors = new List<Error>
        {
            Error.Conflict(),
            Error.Conflict(description: "Another conflict")
        };

        ResultOf result = errors;
        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Fault>();
        result.As<Fault>().Errors.Should().ContainItemsAssignableTo<Error>();
    }
    
    [Fact]
    public void CreateErrorFromImplicitCast()
    {
        var expected = Error.Custom("code", "description");
        ResultOf result = expected;
        
        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Fault>();
        result.As<Fault>().Errors.Should().ContainSingle().Which.Should().Be(expected);
    }

    [Fact]
    public void CreateInvalid()
    {
        var validation = new ValidationError("A", "Code", "Description", ValidationSeverity.Error);
        var result = ResultOf.Invalid(validation);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Invalid>();
        result.As<Invalid>().ValidationErrors.Should().ContainSingle().Which.Should().Be(validation);
    }

    [Fact]
    public void CreateInvalidWithListOfValidationErrors()
    {
        var expected = new List<ValidationError>
        {
            new("A", "Code", "Description", ValidationSeverity.Error),
            new("B", "Code", "Description", ValidationSeverity.Error),
        };
        var result = ResultOf.Invalid(expected);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Invalid>();
        result.As<Invalid>().ValidationErrors.Should().ContainItemsAssignableTo<ValidationError>();
        result.As<Invalid>().ValidationErrors.Should().HaveCount(expected.Count);
    }

    static ResultOf ReturnSuccess<T>(T value) 
    {
        return new SuccessOf<T>(value);
    }

    static ResultOf ReturnNull<T>()
    {
        return null!;
    }
}