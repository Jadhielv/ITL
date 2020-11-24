using FluentValidation.Results;
using System.Collections.Generic;
using System.Text;

namespace KCTest.Infrastructure.Helpers
{
    public static class ValidationsHelper
    {
        public static string ValidationsErrors(this IList<ValidationFailure> failures)
        {
            StringBuilder result = new StringBuilder();

            foreach (var failure in failures)
                result.Append($"* {failure.ErrorMessage},");

            return result.ToString();
        }
    }
}