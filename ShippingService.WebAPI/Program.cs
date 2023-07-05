using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShippingService.RepositoryLayer.Data;
using ShippingService.DomainLayer.Models;
using ShippingService.RepositoryLayer.IRepository;
using ShippingService.RepositoryLayer.Repository;
using ShippingService.ServiceLayer.IServices;
using ShippingService.ServiceLayer.Services;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"])
);
//Add E-mail API Endpoint
builder.Services.AddHttpClient("EmailService", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["EmailService:BaseUri"]);
    httpClient.Timeout = TimeSpan.FromSeconds(30);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler()
    {
        UseDefaultCredentials = true,
        Credentials = new NetworkCredential(builder.Configuration["EmailService:UserName"], builder.Configuration["EmailService:Password"])

    };
});
builder.Services.AddIdentity<WebShopUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<MailService, MailService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IIdentityRepository<>), typeof(IdentityRepository<>));
builder.Services.AddScoped<IService<ShippingHistory>, ShippingHistoryService>();
builder.Services.AddScoped<IIdentityService<WebShopUser>, WebShopUserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Shipping Service API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.Events.OnRedirectToAccessDenied =
        options.Events.OnRedirectToLogin = c =>
        {
            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.FromResult<object>(null);
        };
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])
            ),
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();



