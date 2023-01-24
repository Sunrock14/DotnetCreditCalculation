using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.ComplexTypes;
using System.Text.Json.Serialization;

namespace Credit.Core.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, int statusCode, T data)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
            Data = data;
            
        }
        public DataResult(ResultStatus resultStatus, int statusCode, string message, T data)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, int statusCode, string message, T data, Exception exception)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Exception = exception;
        }

        public ResultStatus ResultStatus { get; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; }
        public Exception Exception { get; }
        public T Data { get; }
    }
}
