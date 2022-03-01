namespace WebApplication1.Commons
{
    /// <summary>
    /// 响应基类 int code + string msg
    /// </summary>
    ///  where T : class
    public class BaseResult
    {
        public int code { get; set; }
        public string msg { get; set; }

        public string[] args { get; set; }

        public BaseResult() { }

        public BaseResult(int code = 0, string msg = "", params string[] args)
        {
            this.code = code;
            this.msg = msg;
            this.args = args;
        }

    }


    /// <summary>
    /// 响应基类 + 泛型对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResult<T> : BaseResult{ 
        public T data { get; set; } = default(T);

        public BaseResult(T data, int code = 0, string msg = "", params string[] args) : base(code, msg, args)
        {
            this.data = data;
        }

    }

}
