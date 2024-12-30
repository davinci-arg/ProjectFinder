using ProjectFinder.Web.Service;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IGitHubAPIService, GitHubAPIService>();
builder.Services.AddHttpClient<IGitHubFinderService, GitHubFinderService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();

// Api services
SD.GitHubSearchRepositoryAPI = builder.Configuration["ServiceUrls:GitHubSearchRepositoryAPI"];
SD.GitHubFinderAPIService = builder.Configuration["ServiceUrls:GitHubFinderAPIService"];
SD.AuthAPIService = builder.Configuration["ServiceUrls:AuthAPIService"];

// Register services
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IBaseService, GitHubService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBaseService, GitHubService>();
builder.Services.AddScoped<IGitHubAPIService, GitHubAPIService>();
builder.Services.AddScoped<IGitHubFinderService, GitHubFinderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GitHubFinder}/{action=Finder}")
    .WithStaticAssets();


app.Run();
