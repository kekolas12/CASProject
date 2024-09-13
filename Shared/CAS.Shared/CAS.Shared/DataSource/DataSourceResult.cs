using System.Collections;
using System.Text.Json.Serialization;

namespace CAS.Shared.DataSource
{
    public class DataSourceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }

        private DataSourceResult(bool isSuccess, T data, string errorMessage, int statusCode)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static DataSourceResult<T> Success(T data, int statusCode) =>
            new DataSourceResult<T>(true, data, null, statusCode);

        public static DataSourceResult<T> Fail(string errorMessage, int statusCode) =>
            new DataSourceResult<T>(false, default, errorMessage, statusCode);
    }
}
