using Microsoft.AspNetCore.Mvc.Filters;

namespace sgc.Controllers.Filters;

public class Validate : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //var headerToken = filterContext.HttpContext.Request.Headers["Authorization"];
        //var tokenUser = JWTUtil.ValidateToken(headerToken!);
        //if (tokenUser == null || tokenUser.Id == 0)
        //{
        //    filterContext.HttpContext.Response.StatusCode = 401;
        //    throw new Exception("Token Inválido!");
        //}
    }
}
