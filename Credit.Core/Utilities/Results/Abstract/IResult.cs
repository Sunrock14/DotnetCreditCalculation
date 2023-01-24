using Credit.Core.Utilities.Results.ComplexTypes;

namespace Credit.Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }

        public int StatusCode { get; set; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
