using System.Text;
using FluentValidation.AspNetCore;
using GegaGamez.BLL;
using GegaGamez.WebUI;
using GegaGamez.WebUI.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Razor pages
builder.Services.AddRazorPages();

// Validation
builder.Services.AddFluentValidation(
    fv =>
    {
        // Validate collections of models
        fv.ImplicitlyValidateRootCollectionElements = true;

        // Validate properties that have registered validators
        fv.ImplicitlyValidateChildProperties = true;
        fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    });

// Add mapping profiles
builder.Services.AddAutoMapper(typeof(MainProfile));

// Add DB
var connectionString = builder.Configuration.GetConnectionString("GegaGamezDev");
builder.Services.AddGegaGamezDB(connectionString);

// Services (interfaces?)
builder.Services.AddGegaGamezServices();

// Auth manager
string key = builder.Configuration.GetSection("SecurityKey").Value;
var userService = builder.Services.AddScoped<IJwtAuthenticationManager>(service =>
{
    return new JwtAuthenticationManager(key);
});

// Auth
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
