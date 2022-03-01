using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Commons;

namespace WebApplication1.Models
{
    /// <summary>
    /// User基类
    /// </summary>
    public class User
    {

        [Column(Name = "id", IsPrimary = true, IsIdentity = true)]
        public int? id { get; set; }

        public string username { get; set; }

        public string? nickname { get; set; }

        public string? icon { get; set; }

        [Column(Name = "create_time")]
        public string? createTime { get; set; }

        public int? forbidden { get; set; } = 0;

        public int? isdelete { get; set; } = 0;

        public User()
        {

        }

        public User(TableUser tu)
        {
            this.id = tu.id;
            this.username = tu.username;
            this.nickname = tu.nickname;
            this.icon = tu.icon;
            this.createTime = tu.createTime;
            this.forbidden = tu.forbidden;
            this.isdelete = tu.isdelete;
        }

    }

    /// <summary>
    /// user表实体类
    /// </summary>
    [Table(Name = "user")]
    public class TableUser : User
    {
        public string password { get; set; }

        public TableUser()
        {
        }

        public TableUser(RegisterUser user, string nickname, string createTime, string? icon = Constant.SERVERURL + "/images/default.png")
        {
            this.username = user.username;
            this.password = user.password;
            this.nickname = nickname;
            this.createTime = createTime;
            this.icon = icon;
        }
    }

    /// <summary>
    /// 注册Bean
    /// </summary>
    public class RegisterUser
    {
        [Required(ErrorMessage = "username can not be empty, plz.", AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string username { get; set; }

        [Required(ErrorMessage = "password can not be empty, plz.", AllowEmptyStrings = false)]
        [Compare("confirmPassword")]
        [MaxLength(18)]
        [MinLength(2)]
        public string password { get; set; }

        [Required(ErrorMessage = "confirmPassword can not be empty, plz.", AllowEmptyStrings = false)]
        public string confirmPassword { get; set; }
    }

    /// <summary>
    /// 登录Bean
    /// </summary>
    public class LoginUser
    {
        [Required(ErrorMessage = "username can not be empty, plz.")]
        public string username { get; set; }

        [Required(ErrorMessage = "password can not be empty, plz.", AllowEmptyStrings = false)]
        public string password { get; set; }
    }

    /// <summary>
    /// User更新Info
    /// </summary>
    public class UpdateUser
    {
        [Required(ErrorMessage = "update user info id can not be empty. plz")]
        public int? id { get; set; }

        public string? nickname { get; set; }

        [Compare("confirmPassword")]
        public string? password { get; set; }

        public string? confirmPassword { get; set; }

    }
}
