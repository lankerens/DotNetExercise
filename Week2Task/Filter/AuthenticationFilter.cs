using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApplication1.Commons;

namespace WebApplication1.Filter
{
    /// <summary>
    /// 鉴权处理类 - 暂时不用
    /// </summary>
    public class AuthenticationFilter : IAuthorizationFilter
    {
        public static List<string> cookies = new List<string>();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Boolean b = context.Filters.Any(item => item is IAllowAnonymousFilter)
                || cookies.Exists(o => string.Equals(o, context.HttpContext.Request.Headers.Cookie));

            if (b)
            {
                return;
            }
            else
            {
                context.Result = new JsonResult(new BaseResult(Constant.FORBIDDEN, "Unauthorized"));
            }

        }
    }
}
