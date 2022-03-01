using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WebApplication1.Commons;
using WebApplication1.Models;
using WebApplication1.Services.Impl;
using Week2Task.Filter;

namespace WebApplication1.Controllers
{

    /// <summary>
    /// UserAPI
    /// </summary>
    [ApiController]     
    [Route("/api/user")]
    [BaseActionFilter] // 参数
    [Authorize]       // 鉴权
    public class UserController : ControllerBase
    {

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<BaseResult> Register([FromBody] RegisterUser user,
            [FromServices] IFreeSql fsql, [FromServices] UserService userService)
        {

            return await userService.Register(user, fsql); ;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        //[ServiceFilter(typeof(BaseResultFilter))]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<BaseResult> Login([FromBody] LoginUser user,
            [FromServices] IFreeSql fsql, [FromServices] UserService userService)
        {
            var oneClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim("Author", "lankerens")
            };
            var oneIdentity = new ClaimsIdentity(oneClaims, "oneIdentity");
            var onePrincipal = new ClaimsPrincipal(new[] { oneIdentity });
            await HttpContext.SignInAsync("CookieAuth", onePrincipal);

            return await userService.Login(user, fsql, Request.Headers.Cookie);
        }


        /// <summary>
        /// 注销-用户退出登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("logout/{id}")]
        public async Task<BaseResult> Logout([FromRoute] int id)
        {
            //todo 
            await HttpContext.SignOutAsync("CookieAuth");

            //清除cookie
            return new BaseResult(Constant.SUCCESS,  "logout success.");
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<BaseResult> Delete([FromRoute] int id,
            [FromServices] IFreeSql fsql, [FromServices] UserService userService)
        {
            // 检验 id => ?
            // todo
            return await userService.Delete(id, fsql);
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<BaseResult> Update([FromBody] UpdateUser user,
            [FromServices] IFreeSql fsql, [FromServices] UserService userService)
        {
            if (user.nickname == null && user.password == null)
            {
                return new BaseResult(Constant.SUCCESS, "like nothing have to change.");
            }

            return await userService.Update(user, fsql);
        }



        /// <summary>
        /// 用户列表分页模糊查询
        /// </summary>
        /// <param name="pageNum">一页多少</param>
        /// <param name="pageNo">第几页</param>
        /// <param name="fsql"></param>
        /// <param name="userService"></param>
        /// <param name="un"></param>
        /// <param name="nn"></param>
        /// <returns></returns>
        [HttpGet("list/{pageNum}/{pageNo}")]
        public async Task<BaseResult> ListSelect([FromRoute] int pageNum, [FromRoute] int pageNo,
            [FromServices] IFreeSql fsql, [FromServices] UserService userService,
            [FromQuery] string? un = "", [FromQuery] string? nn = "")
        {
            //检验 pageNum pageNo =>  ?
            // todo
            return await userService.ListSelect(pageNum, pageNo, un, nn, fsql);
        }


        /// <summary>
        /// id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fsql"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<BaseResult> GetUserById([FromRoute] int id,
            [FromServices] IFreeSql fsql, [FromServices] UserService userService)
        {
            // 校验id => ?
            // todo
            return await userService.GetUserById(id, fsql);
        }


    }
}
