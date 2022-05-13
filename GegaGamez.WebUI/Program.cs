using FluentValidation.AspNetCore;
using GegaGamez.BLL;
using GegaGamez.WebUI;
using GegaGamez.WebUI.MappingProfiles;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

// Razor pages
builder.Services.AddRazorPages();
// controllers
builder.Services.AddControllers()
    .AddJsonOptions(opt=>opt.JsonSerializerOptions.AddDateOnlyConverters());


// Validation
builder.Services
    .AddFluentValidation(
                        fv =>
                        {
                            // Validate collections of models
                            fv.ImplicitlyValidateRootCollectionElements = true;

                            // Validate properties that have registered validators
                            fv.ImplicitlyValidateChildProperties = true;
                            fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
                        })
    // Mapping profiles
    .AddAutoMapper(typeof(MainProfile));

// Add DB
builder.Services.AddGegaGamezDB(builderConfig);

// Add services
builder.Services.AddGegaGamezServices();

// Auth Auth manager
//string securityKey = builder.Configuration.GetSection("SecurityKey").Value;
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

//app.MapGet("/", () => "HelloWorld");

app.Run();
