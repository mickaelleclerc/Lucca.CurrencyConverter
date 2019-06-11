namespace Lucca.Shared.Functional
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Result is being added to support the use case of wanting to consume code
    /// which could generate an error without having to do exception handling.
    ///
    /// https://enterprisecraftsmanship.com/2017/03/13/error-handling-exception-or-result/
    /// </summary>
    /// <remarks>
    /// This implementation doesn't carry any value and use a string to represent what went
    /// wrong in case of a failure outcome.
    /// </remarks>
    public class Result
    {
        private readonly ResultLogic logic;

        internal Result() => this.logic = new ResultLogic();

        internal Result(string errorMessage) => this.logic = new ResultLogic(errorMessage);

        public bool IsOk => this.logic.IsOk;

        public bool IsFailure => this.logic.IsFailure;

        public string ErrorMessage => this.logic.ErrorMessage;
        
        /// <summary>
        /// This static factory is used to build an Ok result which means the calling operation succeed. The resulting
        /// Result instance doesn't carry any value.
        /// </summary>
        /// <returns>An Ok result.</returns>
        public static Result Ok()
        {
            return new Result();
        }

        /// <summary>
        /// This static factory is used to build a Failure result which means something went wrong during operation execution.
        /// </summary>
        /// <param name="errorMessage">An error message describing what went wrong during the operation execution.</param>
        /// <returns>A Failure result.</returns>
        public static Result Failure(string errorMessage)
        {
            return new Result(errorMessage);
        }

        /// <summary>
        /// This static factory is used to build an Ok result which means the calling operation succeed. The resulting
        /// Result instance carries a custom value.
        /// </summary>
        /// <param name="value">The value associated to the result instance.</param>
        /// <typeparam name="TValue">Custom value type.</typeparam>
        /// <returns>A Ok result carrying a value.</returns>
        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return new Result<TValue>(value);
        }

        /// <summary>
        /// This static factory is used to build a Failure result which means something went wrong during operation execution.
        /// </summary>
        /// <param name="errorMessage">An error message describing what went wrong during the operation execution.</param>
        /// <typeparam name="TValue">Custom value type.</typeparam>
        /// <returns>A Failure result.</returns>
        public static Result<TValue> Failure<TValue>(string errorMessage)
        {
            return new Result<TValue>(errorMessage);
        }
    }
    
    /// <summary>
    /// Result is being added to support the use case of wanting to consume code
    /// which could generate an error without having to do exception handling.
    ///
    /// https://enterprisecraftsmanship.com/2017/03/13/error-handling-exception-or-result/
    /// </summary>
    /// <remarks>
    /// This implementation does carry a value and use a string to represent what went
    /// wrong in case of a failure outcome.
    /// </remarks>
    public class Result<TValue> : Result
    {
        private readonly TValue value;

        internal Result(TValue value)
        {
            this.value = value;
        }

        internal Result(string errorMessage)
            : base(errorMessage)
        {
            this.value = default;
        }

        public TValue Value
        {
            [DebuggerStepThrough]
            get
            {
                if (this.IsFailure)
                {
                    throw new InvalidOperationException("A failure cannot have a value");
                }

                return this.value;
            }
        }
    }
}