using FluentValidation.AspNetCore;
using GegaGamez.BLL;
using GegaGamez.WebUI;
using GegaGamez.WebUI.MappingProfiles;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

// Razor pages
builder.Services.AddRazorPages()
    .AddJsonOptions(opt => opt.UseDateOnlyTimeOnlyStringConverters());

// Controllers
builder.Services
    .AddControllers(opt => opt.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(opt => opt.UseDateOnlyTimeOnlyStringConverters());
//.AddJsonOptions(opt => opt.JsonSerializerOptions.AddDateOnlyConverters());

// Validation
builder.Services
    .AddFluentValidation(
                        fv =>
                        {
                            fv.ImplicitlyValidateRootCollectionElements = true;
                            fv.ImplicitlyValidateChildProperties = true;
                            fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
                        });
// Mapping profiles
builder.Services.AddAutoMapper(typeof(MainProfile));

// DB
builder.Services.AddGegaGamezDB(builderConfig);

// Services
builder.Services.AddGegaGamezServices();

// Security: Authentication + Authorization
builder.Services.AddGegaGamezSecurity();

// API
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Todo API",
                Description = "Keep track of your tasks",
                Version = "v1"
            });
    }
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //API
    app.UseSwagger()
        .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GegaGamez v1"));
}

// Configure the HTTP request pipeline.
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication()
    .UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
