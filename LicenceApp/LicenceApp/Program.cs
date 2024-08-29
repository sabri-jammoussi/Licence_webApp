using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.Services.ApplicationService;
using LicenceApp.Services.AttributeLicenceService;
using LicenceApp.Services.AttributeLicenceValuerService;
using LicenceApp.Services.ClientService;
using LicenceApp.Services.Enumeration;
using LicenceApp.Services.EnumerationValeur;
using LicenceApp.Services.LicenceService;
using LicenceApp.Services.PartenaireService;
using LicenceApp.Services.Security;
using LicenceApp.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");
builder.Services.AddDbContext<LicenceDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnections")));


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
    //opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
     opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    //opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

});
// hethi bech t3mel conversion mel decimal lil string !!
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
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IApplicationService , ApplicationService>();
builder.Services.AddScoped<IEnumerationService, EnumerationService>();
builder.Services.AddScoped<IEnumerationValeurService, EnumerationValeurService>();
builder.Services.AddScoped<IAttributeLicenceService, AttributeLicenceService>();
builder.Services.AddScoped<ILicenceService, LicenceService>();
builder.Services.AddScoped<IpartenaireService, PartenaireService>();
builder.Services.AddScoped<IAttributeLicenceValeurService , AttributeLicenceValeurService>();
builder.Services.AddScoped<IPasswordValidator, PasswordValidator>();
builder.Services.AddScoped<IEmailValidator , EmailValidator>();
builder.Services.AddControllers();

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


