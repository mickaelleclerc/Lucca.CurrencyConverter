namespace Lucca.Shared.Functional.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class ResultShould
    {
        [Fact]
        public void BeFailure_Given_ResultIsFailure()
        {
            // Arrange-Act
            var result = Result.Failure("<ErrorMessage>");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsOk.Should().BeFalse();
        }
        
        [Fact]
        public void HasErrorMessage_Given_ResultIsFailure()
        {
            // Arrange-Act
            var result = Result.Failure("<ErrorMessage>");

            // Assert
            result.ErrorMessage.Should().Be("<ErrorMessage>");
        }
        
        [Fact]
        public void ThrowArgumentNullException_Given_FailureWithoutErrorMessage()
        {
            // Arrange-Act
            Action action = () => Result.Failure(string.Empty);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void BeOk_Given_ResultIsOk()
        {
            // Arrange-Act
            var result = Result.Ok();

            // Assert
            result.IsOk.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
        }
        
        [Fact]
        public void ThrowInvalidOperationException_When_ReadErrorMessage_Given_ResultIsOk()
        {
            // Arrange
            var result = Result.Ok();

            // Act
            Func<string> action = () => result.ErrorMessage;
            
            // Assert
            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("There is no error message for success");
        }

        [Fact]
        public void ThrowInvalidOperationException_When_ReadValue_Given_ResultIsFailure()
        {
            // Arrange
            var result = Result.Failure<int>("<ErrorMessage>");

            // Act
            Func<int> action = () => result.Value;

            // Assert
            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("A failure cannot have a value");
        }
    }
}