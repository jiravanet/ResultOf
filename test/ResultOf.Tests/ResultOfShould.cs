namespace ResultOf.Tests;

using FluentAssertions;

public class UnitTest1
{
    [Fact]
    public void CreateSuccessFromValue()
    {
        var result = ReturnSuccess("value");
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("value");
        result.Should().BeOfType<SuccessOf<string>>();
    }

    [Fact]
    public void CreateSuccessFromImplicitCast()
    {
        ResultOf<string> result = "value";
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("value");
        result.Should().BeOfType<SuccessOf<string>>();
    }
    
    [Fact]
    public void CreateFromSuccessValue_ErrorsReturnNoError()
    {
        var result = ReturnSuccess("value");
        result.Errors.Should().ContainSingle().Which.Should().Be(ResultOf<string>.NoError);
    }
    
    [Fact]
    public void CreateFromSuccessValue_ValidationErrorsReturnNoValidationError()
    {
        var result = ReturnSuccess("value");
        result.ValidationErrors.Should().ContainSingle().Which.Should().Be(ResultOf<string>.NoValidationError);
    }
    

    [Fact]
    public void CreateNotFound()
    {
        var result = ResultOf<string>.NotFound();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<NotFound<string>>();
        result.Value.Should().Be(default);
    }
    
    [Fact]
    public void CreateNotFoundOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf<string>.NotFound(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<NotFound<string>>();
        result.Value.Should().Be(default);
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateConflict()
    {
        var result = ResultOf<string>.Conflict();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Conflict<string>>();
        result.Value.Should().Be(default);
    }
    
    [Fact]
    public void CreateConflictOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf<string>.Conflict(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Conflict<string>>();
        result.Value.Should().Be(default);
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateForbidden()
    {
        var result = ResultOf<string>.Forbidden();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Forbidden<string>>();
        result.Value.Should().Be(default);
    }
    
    [Fact]
    public void CreateForbiddenOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf<string>.Forbidden(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Forbidden<string>>();
        result.Value.Should().Be(default);
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateUnauthorized()
    {
        var result = ResultOf<string>.Unauthorized();

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Unauthorized<string>>();
        result.Value.Should().Be(default);
    }
    
    [Fact]
    public void CreateUnauthorizedOwnError()
    {
        var error = Error.Custom("custom", "custom");
        var result = ResultOf<string>.Unauthorized(error);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Unauthorized<string>>();
        result.Value.Should().Be(default);
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }
    
    [Fact]
    public void CreateFault()
    {
        var result = ResultOf<string>.Fault(Error.Fault());

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Fault<string>>();
        result.Errors.Should().ContainSingle().Which.Should().Be(Error.Fault());
        
        result.Value.Should().Be(default);
    }
    
    [Fact]
    public void CreateFaultWithListOfErrors()
    {
        var expected = new List<Error>
        {
            Error.Fault(),
            Error.Fault()
        };
        var result = ResultOf<string>.Fault(expected);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Fault<string>>();
        result.Errors.Should().AllBeAssignableTo<Error>();
        result.Errors.Should().HaveCount(expected.Count);
        result.Value.Should().Be(default);
    }
    
    [Fact]
    public void CreateListOfErrorsFromImplicitCast()
    {
        var errors = new List<Error>
        {
            Error.Conflict(),
            Error.Conflict(description: "Another conflict")
        };

        ResultOf<string> result = errors;
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().Be(default);
        result.Should().BeOfType<Fault<string>>();
        result.Errors.Should().ContainItemsAssignableTo<Error>();
    }
    
    [Fact]
    public void CreateErrorFromImplicitCast()
    {
        var expected = Error.Custom("code", "description");
        ResultOf<string> result = expected;
        
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().Be(default);
        result.Should().BeOfType<Fault<string>>();
        result.Errors.Should().ContainSingle().Which.Should().Be(expected);
    }

    [Fact]
    public void CreateInvalid()
    {
        var validation = new ValidationError("A", "Code", "Description", ValidationSeverity.Error);
        var result = ResultOf<string>.Invalid(validation);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Invalid<string>>();
        result.ValidationErrors.Should().ContainSingle().Which.Should().Be(validation);
        
        result.Value.Should().Be(default);
    }

    [Fact]
    public void CreateInvalidWithListOfValidationErrors()
    {
        var expected = new List<ValidationError>
        {
            new("A", "Code", "Description", ValidationSeverity.Error),
            new("B", "Code", "Description", ValidationSeverity.Error),
        };
        var result = ResultOf<string>.Invalid(expected);

        result.IsSuccess.Should().BeFalse();
        result.Should().BeOfType<Invalid<string>>();
        result.ValidationErrors.Should().ContainItemsAssignableTo<ValidationError>();
        result.ValidationErrors.Should().HaveCount(expected.Count);
        
        result.Value.Should().Be(default);
    }

    static ResultOf<T> ReturnSuccess<T>(T value) 
    {
        return new SuccessOf<T>(value);
    }

    static ResultOf<T> ReturnNull<T>()
    {
        return null!;
    }
}