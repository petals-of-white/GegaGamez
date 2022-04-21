using GegaGamez.BLL.Services;

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

builder.Services.AddScoped(service => new UserService(connectionString));
builder.Services.AddScoped(service => new GameService(connectionString));
builder.Services.AddScoped(service => new GameCollectionService(connectionString));
builder.Services.AddScoped(service => new GenreService(connectionString));
builder.Services.AddScoped(service => new DeveloperService(connectionString));

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
