using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.ComplexTypes;
using System.Text.Json.Serialization;

namespace Credit.Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus,int statusCode)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
        }
        public Result(ResultStatus resultStatus, int statusCode, string message)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
            Message = message;
        }
        public Result(ResultStatus resultStatus, int statusCode, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
            Message = message;
            Exception = exception;
        }
        public ResultStatus ResultStatus { get; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
