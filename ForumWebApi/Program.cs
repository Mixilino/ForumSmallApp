using FluentValidation.AspNetCore;
using ForumWebApi.Data;
using ForumWebApi.Data.AuthRepo;
using ForumWebApi.Data.Interfaces;
using ForumWebApi.Data.UnitOfWork;
using ForumWebApi.Models;
using ForumWebApi.services.CommentService;
using ForumWebApi.services.PostCategoryService;
using ForumWebApi.services.PostService;
using ForumWebApi.services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Security.Claims;
using FluentValidation;
using ForumWebApi.Validators;
using ForumWebApi.Data.PostCategoryRepo;
using Microsoft.AspNetCore.Localization;
using ForumWebApi.Filters;
using Microsoft.Extensions.Localization;
using ForumWebApi.Resources;
using ForumWebApi.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPostCategoryService, PostCategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();

builder.Services.AddDbContext<DataContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header with Bearer scheme, \"bearer {token}\" ",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    
    c.OperationFilter<AddLanguageHeaderParameter>();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<PostCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PostUpdateDtoValidator>();


builder.Services.AddLocalization();


builder.Services.AddControllers()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(ValidationMessages));
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("sr"),
        new CultureInfo("fr")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.Use(async (context, next) =>
{
    var languages = context.Request.Headers["Accept-Language"].ToString();
    if (!string.IsNullOrEmpty(languages))
    {
        // Parse the first language from Accept-Language header
        var primaryLanguage = languages.Split(',')
            .FirstOrDefault()
            ?.Split(';')
            .FirstOrDefault()
            ?.Trim();

        if (!string.IsNullOrEmpty(primaryLanguage))
        {
            try
            {
                var culture = new CultureInfo(primaryLanguage);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                // If culture is not found, fall back to default (en)
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }
    }
    await next();
});

app.UseAuthentication(); // mora ovde pre authorization logicno

app.Use(async (context, next) =>
{
    var identity = context.User.Identity as ClaimsIdentity;
    if (identity != null && identity.Claims.Count() != 0)
    {
        IEnumerable<Claim> claims = identity.Claims;
        context.Items["UserId"] = Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
        context.Items["UserName"] = identity.FindFirst("username")?.Value;
    }
    await next(context);
});

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
