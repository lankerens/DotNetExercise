using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApplication1.Commons;

namespace Week2Task.Filter
{
    /// <summary>
    /// 参数处理类
    /// </summary>
    public class BaseActionFilter : System.Web.Mvc.FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //todo
        }

        /// <summary>
        /// 进入action之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                BaseResult result = new BaseResult(Constant.NOTVALID_PARAMETER, "");

                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors) {
                        // 拼接错误信息
                        result.msg += error.ErrorMessage;
                    }
                }

                context.Result = new JsonResult(result);
            }
        }
    }
}
