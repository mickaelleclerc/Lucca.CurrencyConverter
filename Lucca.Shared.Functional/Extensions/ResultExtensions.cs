namespace Lucca.Shared.Functional.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ResultExtensions
    {
        public static TOutput Match<TValue, TOutput>(
            this Result<TValue> result,
            Func<string, TOutput> failure,
            Func<TValue, TOutput> ok)
        {
            return result.IsFailure
                ? failure(result.ErrorMessage)
                : ok(result.Value);
        }

        public static string JoinErrorMessages(this IEnumerable<Result> results)
        {
            var errorMessages = results
                .Where(result => result.IsFailure)
                .Select(result => result.ErrorMessage)
                .ToList();

            return string.Join(Environment.NewLine, errorMessages);
        }

        public static IReadOnlyCollection<TValue> JoinValues<TValue>(this IEnumerable<Result<TValue>> results)
        {
            return results
                .Where(result => result.IsOk)
                .Select(result => result.Value)
                .ToList();
        }
    }
}