using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.Services;
using LicenceApp.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");
//builder.Services
//    .AddIdentity<UserDao, IdentityRole>(options =>
//    {
//        options.SignIn.RequireConfirmedAccount = false;
//        options.User.RequireUniqueEmail = true;
//        options.Password.RequireDigit = false;
//        options.Password.RequiredLength = 6;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireUppercase = false;
//    })
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<LicenceDBContext>();

builder.Services.AddDbContext<LicenceDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection")));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.EnableAnnotations();
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
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
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddScoped<TokenService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordValidator, PasswordValidator>();
builder.Services.AddScoped<IEmailValidator , EmailValidator>();


// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My first testttttt ");
});
}



app.UseHttpsRedirection();

app.UseRouting();
app.UseStatusCodePages();
app.UseAuthentication();

app.UseAuthorization();
//Routing 
app.UseCors();
app.MapControllers();
//for the (update-databse) will be in the startup of the app 
try
{
    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetService<LicenceDBContext>();
    if (dbContext != null)
    {
        await dbContext.Database.MigrateAsync();
    }
    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


