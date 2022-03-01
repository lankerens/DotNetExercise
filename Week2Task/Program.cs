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

//����swagger_api�ĵ�ע��
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Week2Task API �ĵ�", Version = "v1" });

    // xml�ļ�����·��
    var path = Path.Combine(AppContext.BaseDirectory, "Api.xml");
    // ��ʾ��������ע��
    options.IncludeXmlComments(path, true);
    // ��Action�����ƽ�������
    options.OrderActionsBy(s => s.RelativePath);
});


string connectionString = @"Data Source=127.0.0.1;Port=3306;User ID=root;Password=123456; Initial Catalog=lankerens;Charset=utf8; SslMode=none;Min pool size=1";

// Freesql����
// Initial Catalog => ���ݿ�����
IFreeSql fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.MySql, connectionString)
    .Build();
// Freesql����ע�� => ����ģʽ
builder.Services.AddSingleton<IFreeSql>(fsql);
builder.Services.AddSingleton<UserService>();


builder.Services.AddMvc(op =>
{
    //��������֤������
    //op.Filters.Add<AuthenticationFilter>();

    // �ر��ս��·�ɻ���ʲôӰ�죿
    op.EnableEndpointRouting = false;
});
//builder.Services.AddSingleton<BaseResultFilter>();


// �ر��Զ���֤ �߹�����������֤
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(op =>
{
    op.SuppressModelStateInvalidFilter = true;
});


// Cookie�м������.
//�����Ȩ�����ʹ��Cookie�ķ�ʽ
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
