using System.Net;

namespace sgc.Domain.Entities.Handlers;

public abstract class Result(bool isSuccess, string message)
{
    public bool IsSuccess { get; private set; } = isSuccess;
    public string Message { get; private set; } = message;
}

public class ResultData<T>(T? data, bool isSuccess = true, string message = "", HttpStatusCode statusCode = HttpStatusCode.OK) : Result(isSuccess, message)
{
    public T? Data { get; private set; } = data;
    public HttpStatusCode StatusCode { get; private set; } = statusCode;

    public static ResultData<T> Success(T data) => new(data);
    public static ResultData<T> Failure(string message, HttpStatusCode statusCode) => new(default, false, message, statusCode);
    public TResult IsMatch<TResult>(Func<ResultData<T>, TResult> onSuccess, Func<ResultData<T>, TResult> onFailure) =>
        IsSuccess ? onSuccess(this) : onFailure(this);
}
