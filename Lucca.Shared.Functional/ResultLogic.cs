namespace Lucca.Shared.Functional
{
    using System;
    using System.Diagnostics;

    internal class ResultLogic
    {
        private readonly string errorMessage;

        public bool IsOk => !this.IsFailure;

        public bool IsFailure { get; }

        public string ErrorMessage
        {
            [DebuggerStepThrough]
            get
            {
                if (this.IsOk)
                {
                    throw new InvalidOperationException("There is no error message for success");
                }

                return this.errorMessage;
            }
        }

        internal ResultLogic()
        {
            this.IsFailure = false;
            this.errorMessage = string.Empty;
        }

        internal ResultLogic(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage), "Error message must be provided for failure");
            }

            this.IsFailure = true;
            this.errorMessage = errorMessage;
        }
    }
}