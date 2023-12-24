using System;

namespace EMS.Shared.Application.Wrapper
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        protected Result(T value, bool isSuccess, string errorMessage)
        {
            Value = value;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty);

        public static Result<T> Fail(string errorMessage = "An error occurred.")
            => new Result<T>(default, false, errorMessage);

        public void Deconstruct(out bool success, out T value, out string error)
        {
            success = IsSuccess;
            value = Value;
            error = ErrorMessage;
        }
    }
}
