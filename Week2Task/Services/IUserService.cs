using WebApplication1.Commons;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        Task<BaseResult> Register(RegisterUser user, IFreeSql fsql);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        Task<BaseResult> Login(LoginUser user, IFreeSql fsql, string cookie);


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        Task<BaseResult> Delete(int id, IFreeSql fsql);

        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        Task<BaseResult> Update(UpdateUser user, IFreeSql fsql);

        /// <summary>
        /// 用户列表分页模糊查询
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageNo"></param>
        /// <param name="un"></param>
        /// <param name="nn"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        Task<BaseResult> ListSelect(int pageNum, int pageNo, string un, string nn, IFreeSql fsql);


        /// <summary>
        /// id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        Task<BaseResult> GetUserById(int id, IFreeSql fsql);

    }
}
