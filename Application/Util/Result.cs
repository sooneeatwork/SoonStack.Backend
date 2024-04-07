using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Util
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(T value, bool isSuccess, Error error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure(Error error) => new Result<T>(default, false, error);
    }

    public class Error
    {
        public string Message { get; }
        public Error(string message)
        {
            Message = message;
        }
    }

}
