using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication1.Data;
using WebApplication1.Repositories.Authentication;
using WebApplication1.Repositories.broker;
using WebApplication1.Repositories.TradingAccount;
using WebApplication1.Repositories.BankUserRepo;
using WebApplication1.Repositories.TransactionRepo;
using WebApplication1.Repositories.UserCryptoRepo;
using WebApplication1.Services.Authentication;
using WebApplication1.Services.Broker;
using WebApplication1.Services.Email;
using WebApplication1.Services.TransactionService;
using WebApplication1.Services.UserBankAccPaymentMethod;
using WebApplication1.Services.UserCryptoService;
using WebApplication1.Services.WithdrawService;

// DB timestamp columns are "timestamp without time zone"; map DateTime to them
// and relax Npgsql's strict DateTimeKind enforcement (must be set before any Npgsql use).
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddAuthentication().AddCookie();

// Compress text responses (HTML/CSS/JS) — large payloads shrink ~70-80%, a big win on mobile.
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
    options.MimeTypes = Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/javascript", "text/javascript", "image/svg+xml", "application/json" });
});
builder.Services.Configure<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProviderOptions>(o =>
    o.Level = System.IO.Compression.CompressionLevel.Fastest);
builder.Services.Configure<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProviderOptions>(o =>
    o.Level = System.IO.Compression.CompressionLevel.Fastest);

// Add services to the container.
builder.Services.AddControllersWithViews();
// In-memory cache (used for shared reference data such as the broker list)
builder.Services.AddMemoryCache();
//Register DB
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
 builder.Services.AddScoped<IUserDetailRepository,UserDetailDetailRepositoryImp>();
 builder.Services.AddScoped<IBrokerRepository, BrokerRepositoryImp>();
 builder.Services.AddScoped<IUserTradingAccountRepo, UserTradingAccountRepoImp>();
 builder.Services.AddScoped<IUserCryptoRepo, UserCryptoRepoImp>();
 builder.Services.AddScoped<IBankUserRepo, BankUserRepoImp>();
 builder.Services.AddScoped<ITransactionRepo, TransactionRepoImp>();
 builder.Services.AddScoped<ITransactionUnauthRepo, TransactionUnauthRepoImp>();

 //Register Services
 builder.Services.AddScoped<IUserDetailService, UserDetailServiceImp>();
 builder.Services.AddScoped<IAuthentication, AuthenticationImp>();
 builder.Services.AddScoped<IBrokerService, BrokerServiceImp>();
 builder.Services.AddScoped<ITradingAccount, TradingAccountImp>();
 builder.Services.AddScoped<IUserCryptoService, UserCryptoServiceImp>();
 builder.Services.AddScoped<IBankUserService, BankUserServiceImp>();
 builder.Services.AddScoped<IWithdrawService, WithdrawServiceImp>();
 builder.Services.AddScoped<ITransactionService, TransactionServiceImp>();

 // Email (SMTP via Brevo) for password-reset messages
 builder.Services.Configure<EmailSettings>(config.GetSection("SmtpSettings"));
 builder.Services.AddTransient<IEmailSender, EmailSender>();


//Register Identity
// builder.Services.AddIdentity<IdentityUser, IdentityRole>();


// 2. Add Identity
 builder.Services.AddIdentity<IdentityUser, IdentityRole>()
     .AddEntityFrameworkStores<AppDBContext>() 
     .AddDefaultTokenProviders();


// // Register Serilog
// Log.Logger = new LoggerConfiguration()
//     .Enrich.WithThreadId()
//     .Enrich.FromLogContext()
//     .ReadFrom.Configuration(config)
//     .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}






app.UseHttpsRedirection();

app.UseResponseCompression();

// Disable caching during development so views and static assets (CSS/JS) always reload fresh
if (app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            context.Response.Headers["Pragma"] = "no-cache";
            context.Response.Headers["Expires"] = "0";
            return Task.CompletedTask;
        });
        await next();
    });
}

// Serve static assets (css/js/img) with long-lived browser caching in production so
// repeat visits — especially on mobile — don't re-download them every time.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
}
else
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // 30 days, immutable; safe as long as changed assets get a new name/query.
            ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=2592000,immutable";
        }
    });
}
app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();