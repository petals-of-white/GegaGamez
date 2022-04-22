using System.Text;
using GegaGamez.BLL.Services;
using GegaGamez.WebUI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// connectino string
string connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddScoped(service => new GameService(connectionString));
//builder.Services.AddScoped(service => new CollectionsBL(connectionString));
//builder.Services.AddScoped(service => new CommentsBL(connectionString));
//builder.Services.AddScoped(service => new GenreBL(connectionString));
//builder.Services.AddScoped(service => new RatingBL(connectionString));
//builder.Services.AddScoped(service => new UserService(connectionString));
//builder.Services.AddScoped(service => new DeveloperBL(connectionString));

// services
builder.Services.AddScoped(service => new UserService(connectionString));
builder.Services.AddScoped(service => new UserAuthService(connectionString));
builder.Services.AddScoped(service => new GameService(connectionString));
builder.Services.AddScoped(service => new GameCollectionService(connectionString));
builder.Services.AddScoped(service => new GenreService(connectionString));
builder.Services.AddScoped(service => new DeveloperService(connectionString));

// auth manager
string key = builder.Configuration.GetSection("SecurityKey").Value;
var userService = builder.Services.AddScoped<IJwtAuthenticationManager>(service =>
{
    return new JwtAuthenticationManager(service.GetService<UserAuthService>()!, key);
});

// auth
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies ["access_token"];
            return Task.CompletedTask;
        }
    };
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
