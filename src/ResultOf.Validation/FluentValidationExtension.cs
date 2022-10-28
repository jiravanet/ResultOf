namespace ResultOf.Validation;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

public static class FluentValidationExtension
    {
        public static IEnumerable<ValidationError> AsErrors(this ValidationResult results)
        {
            var result = results.Errors
                .Select(error =>
                    new ValidationError(error.PropertyName, error.ErrorCode, error.ErrorMessage,
                        FromSeverity(error.Severity))).ToList();

            return result.AsReadOnly();
        }

        static ValidationSeverity FromSeverity(Severity severity)
        {
            return severity switch
            {
                Severity.Error => ValidationSeverity.Error,
                Severity.Warning => ValidationSeverity.Warning,
                Severity.Info => ValidationSeverity.Info,
                _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null)
            };
        }
    }