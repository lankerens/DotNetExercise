using Newtonsoft.Json;
using WebApplication1.Commons;
using WebApplication1.Filter;
using WebApplication1.Models;


namespace WebApplication1.Services.Impl
{
    /// <summary>
    /// 用户处理实现类
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        public async Task<BaseResult> Delete(int id, IFreeSql fsql)
        {
            int rows = fsql.Update<TableUser>().Set(a => new { isdelete = 1 }).Where(a => a.id == id).ExecuteAffrows();
            if (rows < 1)
            {
                return new BaseResult(Constant.FAILURE, "wait, delete error.");
            }

            return new BaseResult(Constant.SUCCESS, "account, delete success.");
        }

        public async Task<BaseResult> GetUserById(int id, IFreeSql fsql)
        {
            var u = fsql.Select<User>()
                .Where(a => a.id == id).ToOne();
            if (u == null) return new BaseResult(Constant.NOT_FOUND, "like not found this user by id.");
            return new BaseResult<User>(u, Constant.SUCCESS, "get user by id success.");
        }

        /// <summary>
        /// 用户列表分页模糊查询
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageNo"></param>
        /// <param name="un"></param>
        /// <param name="nn"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        public async Task<BaseResult> ListSelect(int pageNum, int pageNo, string un, string nn, IFreeSql fsql)
        {
            var ulist = fsql.Select<User>()
                .Where(a => a.isdelete == 0 && a.username.Contains(un) && a.nickname.Contains(nn))
                .OrderBy(a => a.id)
                .Count(out var total)
                .Page(pageNo, pageNum)
                .ToList();

            //return new BaseResult();
            return new BaseResult<List<User>>(ulist, Constant.SUCCESS, "ListSelect success.", total.ToString());
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        public async Task<BaseResult> Login(LoginUser user, IFreeSql fsql, string cookie)
        {
            var tu = fsql.Select<TableUser>().Where(a => a.username == user.username && a.isdelete == 0).ToOne();

            if (tu == null)
            {
                return new BaseResult(Constant.FORBIDDEN, "account have not registed");
            }
            else
            {
                // 比较
                if (string.Equals(tu.password, user.password))
                {
                    // todo 
                    // 添加cookie
                    //AuthenticationFilter.cookies.Add(cookie);

                    // 返回登录的用户基本信息
                    return new BaseResult<User>(new User(tu), Constant.SUCCESS, "yep, login success.");
                }
                else
                {
                    return new BaseResult(Constant.NOT_ACCEPTABLE, "account or password Incorrect");
                }
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        public async Task<BaseResult> Register(RegisterUser user, IFreeSql fsql)
        {
            var repo = fsql.GetRepository<TableUser>();
            // 判断
            var tu = repo.Where(a => a.username == user.username && a.isdelete == 0).ToOne();

            if (tu == null)
            {
                // 插入
                tu = new TableUser(user, "游客" + Guid.NewGuid().ToString().Substring(0, 7),
                    DateTime.Now.ToString());
                repo.Insert(tu);
            }
            else
            {
                // 已存在
                return new BaseResult(Constant.IS_EXIST, Constant.IS_EXIST_USER);
            }

            return new BaseResult<User>(new User(tu), Constant.SUCCESS, "register success");
        }


        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fsql"></param>
        /// <returns></returns>
        public async Task<BaseResult> Update(UpdateUser user, IFreeSql fsql)
        {
            var repo = fsql.GetRepository<TableUser>();
            var tu = repo.Where(a => a.id == user.id).ToOne();
                tu.nickname = user.nickname == null ? tu.nickname : user.nickname;
                tu.password = user.password == null ? tu.password : user.password;

            int row = repo.Update(tu);

            return new BaseResult<User>(new User(tu), Constant.SUCCESS, "update user info success.");
        }
    }
}
