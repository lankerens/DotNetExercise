using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApplication1.Commons;

namespace Week2Task.Filter
{

    /// <summary>
    /// 响应拦截处理类.....暂时不用.
    /// </summary>
    public class BaseResultFilter : System.Web.Mvc.FilterAttribute, IResultFilter
    {

        // called after the action result executes
        // 在操作结果执行后调用
        public void OnResultExecuted(ResultExecutedContext context)
        {
            ////Console.WriteLine(JsonConvert.SerializeObject(context.Result));
        }

        // called before the action result executes
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var obj = Newtonsoft.Json.Linq.JObject.Parse(JsonConvert.SerializeObject(context.Result));
            if (obj["Value"]["code"] == null)
            {
                string msg = obj["Value"]["Errors"].ToString();
                int code = Convert.ToInt32(obj["StatusCode"].ToString());

                Console.WriteLine(msg);
                Console.WriteLine(context.HttpContext.Response.HasStarted); 

                // 自定义返回数据
                context.Result = new JsonResult(new BaseResult(code, msg));
            }

        }
    }
}
