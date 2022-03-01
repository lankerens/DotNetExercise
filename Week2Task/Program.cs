using Microsoft.OpenApi.Models;
using WebApplication1.Commons;
using WebApplication1.Filter;
using WebApplication1.Services.Impl;
using Week2Task.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//配置swagger_api文档注释
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Week2Task API 文档", Version = "v1" });

    // xml文件绝对路径
    var path = Path.Combine(AppContext.BaseDirectory, "Api.xml");
    // 显示控制器的注释
    options.IncludeXmlComments(path, true);
    // 对Action的名称进行排序
    options.OrderActionsBy(s => s.RelativePath);
});


string connectionString = @"Data Source=127.0.0.1;Port=3306;User ID=root;Password=123456; Initial Catalog=lankerens;Charset=utf8; SslMode=none;Min pool size=1";

// Freesql配置
// Initial Catalog => 数据库名称
IFreeSql fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.MySql, connectionString)
    .Build();
// Freesql依赖注入 => 单例模式
builder.Services.AddSingleton<IFreeSql>(fsql);
builder.Services.AddSingleton<UserService>();


builder.Services.AddMvc(op =>
{
    //添加身份验证过滤器
    //op.Filters.Add<AuthenticationFilter>();

    // 关闭终结点路由会有什么影响？
    op.EnableEndpointRouting = false;
});
//builder.Services.AddSingleton<BaseResultFilter>();


// 关闭自动验证 走过滤器进行验证
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(op =>
{
    op.SuppressModelStateInvalidFilter = true;
});


// Cookie中间件配置.
//添加授权，添加使用Cookie的方式
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "Lankerens.Cookie";
        config.Events.OnRedirectToLogin = context =>
        {
            context.Response.WriteAsJsonAsync<BaseResult>(new BaseResult(StatusCodes.Status401Unauthorized, "Unauthorized"));
            return Task.CompletedTask;
        };
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

// who are you?
app.UseAuthentication();

// are you allowed?
app.UseAuthorization();

app.MapControllers();

app.Run();
