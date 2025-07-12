using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace sgc.Domain.Entities.Handlers;

public abstract class ResultBase(bool isSuccess, string message)
{
    public bool IsSuccess { get; } = isSuccess;
    public string Message { get; } = message;
    public HttpStatusCode StatusCode { get; set; }

    public abstract IActionResult ToActionResult();
}

public class ResultData<T>(T? data, bool isSuccess = true, string message = "") : ResultBase(isSuccess, message)
{
    public T? Data { get; private set; } = data;


    public override IActionResult ToActionResult()
    {
        if (IsSuccess)
        {
            return StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(new { data = Data, success = true, message = Message }),
                HttpStatusCode.Created => new ObjectResult(new { success = true, message = Message }) { StatusCode = 201 },
                HttpStatusCode.NoContent => new ObjectResult(new { success = true, message = Message }) { StatusCode = 204 },
                _ => new OkObjectResult(new { data = Data, success = true, message = Message })
            };
        }

        return StatusCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestObjectResult(new { success = false, message = Message }),
            HttpStatusCode.NotFound => new NotFoundObjectResult(new { success = false, message = Message }),
            HttpStatusCode.Unauthorized => new ObjectResult(new { success = false, message = Message }) { StatusCode = 401 },
            HttpStatusCode.Forbidden => new ObjectResult(new { success = false, message = Message }) { StatusCode = 403 },
            HttpStatusCode.Conflict => new ConflictObjectResult(new { success = false, message = Message }),
            _ => new BadRequestObjectResult(new { success = false, message = Message })
        };
    }

    public static ResultData<T> Success(T data, string message = "", HttpStatusCode statusCode = HttpStatusCode.OK) => 
        new(data, true, message) { StatusCode = statusCode };
    
    public static ResultData<T> Failure(string message, HttpStatusCode statusCode) => 
        new(default, false, message) { StatusCode = statusCode };
}
